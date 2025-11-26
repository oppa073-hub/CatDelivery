using UnityEngine;

public class BossStatePattern : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectRange = 8f;   // 플레이어 인식 거리
    [SerializeField] private int maxHp = 5;
    [SerializeField] private SkillBase[] skills;
    [SerializeField] private GameObject goal;

    [SerializeField] private Animator animator;
    private SpriteRenderer spriteRenderer;

    private IEnemyState _currentState;

    public Transform Player => player;
    public float MoveSpeed => moveSpeed;
    public float DetectRange => detectRange;
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
        foreach (var skill in skills)
        {
            if (skill != null)
                skill.Init(this);
        }

        // 시작은 대기 상태
        SetState(new BossIdleState(this));
        goal.SetActive(false);
        
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
            goal.SetActive(true);
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    private void Die()
    {
        // 죽는 연출 넣고, 끝나면 비활성화 등
        animator.SetTrigger("Hurt");
        gameObject.SetActive(false);
    }
    

    public void SetState(IEnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
