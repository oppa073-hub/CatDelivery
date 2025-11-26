using UnityEngine;

public class BossPhaseState : IEnemyState
{
    private BossStatePattern boss;
    private Transform player;
    private float moveSpeed;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float skillCooldown = 3f;   // 스킬 사용 주기
    private float skillTimer = 0f;

    public BossPhaseState(BossStatePattern Boss)
    {
        boss = Boss;
        player = boss.Player;
        moveSpeed = boss.MoveSpeed;
        animator = boss.Animator;
        spriteRenderer = boss.SpriteRenderer;
    }
    public void Enter()
    {
        animator.SetBool("IsMoving", true);
    }
    public void Exit()
    {
        animator.SetBool("IsMoving", false);
    }
    public void Update()
    {
        if (player == null) return;

        // 이동 방향
        Vector3 dir = (player.position - boss.transform.position).normalized;

        // 좌우 방향 전환 (플레이어처럼)
        if (!Mathf.Approximately(dir.x, 0f))
        {
            spriteRenderer.flipX = dir.x < 0f;
        }

        boss.transform.position += dir * moveSpeed * Time.deltaTime;

        skillTimer += Time.deltaTime;
        if (skillTimer >= skillCooldown)
        {
            skillTimer = 0f; // 스킬 발동 (아직 실제 구현 X)
            animator.SetTrigger("Cast");
            boss.UseSkill(0); // 0번 스킬 사용 (예시)
        }

    }
}
