
using UnityEngine;

public class BossBallSkill : SkillBase
{
    [SerializeField] private int poolIndex = 0;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speed = 8f;

    public override void Cast()
    {
        //  풀에서 공 오브젝트 꺼내오기
        GameObject ball = PoolManager.Instance.Get(poolIndex);
        if (ball == null) return;

        ball.transform.position = firePoint.position;
        ball.transform.rotation = Quaternion.identity;

        var proj = ball.GetComponent<BossBallProjectile>();
        if (proj != null)
        {
            //  플레이어 방향 벡터 계산 (owner = 보스)
            Vector2 dir = (owner.Player.position - owner.transform.position).normalized;

            // 발사 방향, 속력 전달
            proj.Init(dir, speed);
        }
    }
}
