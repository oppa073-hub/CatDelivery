using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PatrolState : IEnemyState
{
    private EnemyStatePattern enemy;
    private int currentPatrolIndex = 0;
    private Transform[] patrolPoints;
    private float moveSpeed;
    private float detectRange;
    private Transform player;

    public PatrolState(EnemyStatePattern Enemy)
    {
        enemy = Enemy;
        patrolPoints = enemy.PatrolPoints;
        moveSpeed = enemy.MoveSpeed;
        player = enemy.PlayerTrf;
        detectRange = enemy.DetectRange;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
        if (patrolPoints.Length == 0)
            return;
        Transform target = patrolPoints[currentPatrolIndex];

        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position,
           target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(enemy.transform.position, target.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        //플레이어 감지 및 상태 변경
        if (Vector2.Distance(enemy.transform.position, player.position) <= detectRange)
        {
            enemy.SetState(new ChaseState(enemy));
        }
    }
}
