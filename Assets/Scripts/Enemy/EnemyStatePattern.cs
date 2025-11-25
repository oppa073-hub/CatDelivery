using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyStatePattern : MonoBehaviour
{
    public Transform player;
    public Transform[] patrolPoints;
    public float detectRange = 5f;
    public float moveSpeed = 2f;
    public float chaseSpeed = 5f;

    private IEnemyState currentState;
    public Transform[] PatrolPoints => patrolPoints;
    public float MoveSpeed => moveSpeed;
    public float DetectRange => detectRange;
    public Transform PlayerTrf => player;
    public float ChaseSpeed => chaseSpeed; 

    private Animator animator;
    public Animator Animator => animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetState(new PatrolState(this));
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Playing) return;
        currentState.Update();
    }

    public void SetState(IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

}
