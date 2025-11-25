using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using System.Collections;

public class PlayerStatePattern : MonoBehaviour
{
    [SerializeField] private int maxHp = 6;
    [SerializeField] private float invincibleDuration = 0.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float dashSpeed = 20f;

    private int currentHp;
    private bool isInvincible;
    private float invincibleTimer;

    public bool CanInput { get; private set; } = true;

    private Rigidbody2D rigid;      
    private Vector2 moveInput;

    private bool isGround;           
    private bool jumpPressed;
    private bool isJumping;

    private bool dashPressed;

    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public LayerMask GroundLayer => groundLayer;             // (타입은 LayerMask가 더 적절하지만, 여기선 코드 그대로 둠)
    public Transform GroundChecker => groundChecker;
    public float GroundCheckDistance => groundCheckDistance;
    public bool JumpPressed => jumpPressed;
    public bool DashPressed => dashPressed;
    public Rigidbody2D Rigidbody => rigid;
    public bool IsGround => isGround;
    public bool IsJumping => isJumping;
    public Vector2 MoveInput => moveInput;
    public int CurrentHp => currentHp;
    public bool IsInvincible => isInvincible;
    public float DashSpeed => dashSpeed;

    private int jumpCount;
    public int JumpCount => jumpCount;
    public int MaxJumpCount => maxJumpCount;
    public void ResetJumpCount() => jumpCount = 0;
    public void AddJumpCount() => jumpCount++;

    private IPlayerState currentState; // 현재 플레이어 상태 객체

    private Animator animator;
    public Animator Animator => animator;

    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction dashAction;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();             // Rigidbody2D 캐싱
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHp = maxHp;

        var action = playerInput.actions;               // PlayerInput에서 액션맵 가져오기
        moveAction = action["MoveAction"];              // "MoveAction" 이름의 액션
        jumpAction = action["JumpAction"];              // "JumpAction" 이름의 액션
        dashAction = action["DashAction"];
    }
    private void Start()
    {
        // 시작 상태는 Idle
        SetState(new IdleStat(this));
    }
    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.State != GameState.Playing)
            return;
       
        // 1) 입력 읽기
        ReadInput();

        // 2) 땅 체크
        GroundCheck();

        // 3) 현재 상태의 Update 호출
        currentState?.Update();

        // 4) Jump 입력은 1프레임짜리 플래그이므로 처리 후 초기화
        jumpPressed = false;
        dashPressed = false;
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <=  0f)
            {
                isInvincible = false;
            }

        }
    }

    private void ReadInput()
    {
        // MoveAction의 Vector2 값 읽기 (x: 좌우, y: 필요하면 위아래)
        moveInput = moveAction.ReadValue<Vector2>();

        // 이번 프레임에 JumpAction이 눌렸는지 확인
        if (jumpAction.WasPerformedThisFrame())
        {
            jumpPressed = true;
        }
        if (dashAction.WasPerformedThisFrame())
        {
            dashPressed = true;
        }
    }

    private void GroundCheck()
    {
        // 발 위치에서 아래로 Ray를 쏴서 땅과 충돌했는지 확인
        isGround = Physics2D.Raycast(
            groundChecker.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer);

        animator.SetBool("IsGround", isGround);

        if (IsGround && Rigidbody.linearVelocity.y <= 0)
        {
            ResetJumpCount();
        }
    }

    public void TakeDamage(int dmg)
    {
        if (isInvincible) return;
        currentHp -= dmg;
        Debug.Log(currentHp);

        if (currentHp <= 0)
        {
            currentHp = 0;

            animator.SetBool("Dead", true);

            // 죽는 상태로 전환
            SetState(new DeadState(this));

            // 게임매니저에 알림
            GameManager.Instance.GameOver();

            return;
        }

        isInvincible = true;
        invincibleTimer = invincibleDuration;

        SetState(new HurtState(this));
    }
    public void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        CanInput = false;
        yield return new WaitForSeconds(duration);
        CanInput = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") ||
            collision.collider.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            SetState(new ClearState(this));
            GameManager.Instance.StageClear();
        }
        else if (collision.CompareTag("DeathZone"))
        {
            // 즉사 낙사 처리
            currentHp = 0;
            SetState(new DeadState(this));
            GameManager.Instance.GameOver();
        }
    }

    public void SetState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
