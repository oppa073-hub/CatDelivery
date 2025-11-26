using UnityEngine;

public class BossStatePattern : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float aggroRange = 8f;   // 플레이어 인식 거리
    [SerializeField] private int maxHp = 5;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private IEnemyState _currentState;

    public Transform Player => player;
    public float MoveSpeed => moveSpeed;
    public float AggroRange => aggroRange;
    public int MaxHp => maxHp;
    public int CurrentHp { get; private set; }

    public Animator Animator => animator;
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    private void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        CurrentHp = maxHp;
    }

    private void Start()
    {
        // 시작은 대기 상태
        SetState(new BossIdleState(this));
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Playing) return;
        _currentState?.Update();
    }
    public void TakeDamage(int dmg)
    {
        CurrentHp -= dmg;
        if (CurrentHp <= 0)
        {
            Die();
        }
        else
        {
            //animator.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        // 죽는 연출 넣고, 끝나면 비활성화 등
        //animator.SetTrigger("Die");
        // 여기서 콜라이더 꺼주거나 게임 승리 처리
    }
    public void UseSkill(int skillIndex)
    {
  
        // 스킬 종류가 적으면 switch로 바로 처리해도 됨.
        // 지금은 뼈대만:
        Debug.Log($"Boss Use Skill: {skillIndex}");
    }
    public void SetState(IEnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
