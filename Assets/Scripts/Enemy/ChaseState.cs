using UnityEngine;

public class ChaseState : IEnemyState
{
    private EnemyStatePattern enemy;
    private Transform player;
    private float chaseSpeed;
    private float detectRange;
    private Animator animator;
    public ChaseState(EnemyStatePattern Enemy)
    {
        enemy = Enemy;
        player = enemy.PlayerTrf;
        chaseSpeed = enemy.ChaseSpeed;
        detectRange = enemy.DetectRange;
        animator = enemy.Animator;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Update()
    {
        Vector3 direction = (player.position - enemy.transform.position).normalized;
        Vector3 nextPos = new Vector3(enemy.transform.position.x + direction.x * chaseSpeed * Time.deltaTime,
            enemy.transform.position.y);
        enemy.transform.position = nextPos;

        //플레이어가 범위를 벗어나면 Patrol 상태로 전이
        if (Vector2.Distance(enemy.transform.position, player.position) > detectRange)
        {
            animator.SetBool("Chaseing", false);
            enemy.SetState(new PatrolState(enemy));
        }
    }
}
