using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
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

    private int currentHp;
    private bool isInvincible;
    private float invincibleTimer;


    private Rigidbody2D rigid;      
    private Vector2 moveInput;

    private bool isGround;           
    private bool jumpPressed;
    private bool isJumping;

    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public LayerMask GroundLayer => groundLayer;             // (타입은 LayerMask가 더 적절하지만, 여기선 코드 그대로 둠)
    public Transform GroundChecker => groundChecker;
    public float GroundCheckDistance => groundCheckDistance;
    public bool JumpPressed => jumpPressed;
    public Rigidbody2D Rigidbody => rigid;
    public bool IsGround => isGround;
    public bool IsJumping => isJumping;
    public Vector2 MoveInput => moveInput;
    public int CurrentHp => currentHp;
    public bool IsInvincible => isInvincible;


    private int jumpCount;
    public int JumpCount => jumpCount;
    public int MaxJumpCount => maxJumpCount;
    public void ResetJumpCount() => jumpCount = 0;
    public void AddJumpCount() => jumpCount++;

    private IPlayerState currentState; // 현재 플레이어 상태 객체

    private InputAction moveAction;
    private InputAction jumpAction;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();             // Rigidbody2D 캐싱

        currentHp = maxHp;

        var action = playerInput.actions;               // PlayerInput에서 액션맵 가져오기
        moveAction = action["MoveAction"];              // "MoveAction" 이름의 액션
        jumpAction = action["JumpAction"];              // "JumpAction" 이름의 액션
    }
    private void Start()
    {
        // 시작 상태는 Idle
        SetState(new IdleStat(this));
    }
    private void Update()
    {
        // 1) 입력 읽기
        ReadInput();

        // 2) 땅 체크
        GroundCheck();

        // 3) 현재 상태의 Update 호출
        currentState?.Update();

        // 4) Jump 입력은 1프레임짜리 플래그이므로 처리 후 초기화
        jumpPressed = false;

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
    }

    private void GroundCheck()
    {
        // 발 위치에서 아래로 Ray를 쏴서 땅과 충돌했는지 확인
        isGround = Physics2D.Raycast(
            groundChecker.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer);
        if (IsGround && Rigidbody.linearVelocity.y <= 0)
        {
            ResetJumpCount();
        }
    }

    public void TakeDamage(int dmg)
    {
        if (isInvincible) return;
        currentHp -= dmg;

        if (currentHp <= 0)
        {
            currentHp = 0;

            // 게임매니저에 알림
            //GameManager.Instance.OnPlayerDead();

            // 죽는 상태로 전환
            SetState(new DeadState(this));
            return;
        }

        isInvincible = true;
        invincibleTimer = invincibleDuration;

        SetState(new HurtState(this));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") ||
            collision.collider.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
    }

    public void SetState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
