using UnityEngine;

public class BossIdleState : IEnemyState
{
    private BossStatePattern boss;
    private Transform player;
    private float aggroRange;
    private Animator animator;
    public BossIdleState(BossStatePattern Boss)
    {
        boss = Boss;
        player = boss.Player;
        aggroRange = boss.AggroRange;
        animator = boss.Animator;
    }
    public void Enter()
    {
        // 대기 모션, 인트로 모션 등
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
         if (player == null) return;

        float dist = Vector2.Distance(boss.transform.position, player.position);

        // 플레이어가 어그로 범위에 들어오면 1페이즈 시작
        if (dist <= aggroRange)
        {
            boss.SetState(new BossPhaseState(boss));
        }
    }
}
