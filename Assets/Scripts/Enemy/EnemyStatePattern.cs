using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyStatePattern : MonoBehaviour
{
    public Transform player;
    public Transform[] patrolPoints;
    public float detectRange = 5f;
    public float moveSpeed = 2f;

    private IEnemyState currentState;
    public Transform[] PatrolPoints => patrolPoints;
    public float MoveSpeed => moveSpeed;
    public float DetectRange => detectRange;
    public Transform PlayerTrf => player;

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
