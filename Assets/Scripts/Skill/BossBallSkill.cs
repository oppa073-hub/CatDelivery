using UnityEditor.EditorTools;
using UnityEngine;

public class BossBallSkill : SkillBase
{
    [SerializeField] private int poolIndex = 1;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speed = 8f;

    public override void Cast()
    {
        // 1) 풀에서 공 오브젝트 꺼내오기
        GameObject ball = PoolManager.Instance.Get(poolIndex);
        if (ball == null) return;

        ball.transform.position = firePoint.position;
        ball.transform.rotation = Quaternion.identity;

        // 2) 프로젝트일 초기화
        var proj = ball.GetComponent<BossBallProjectile>();
        if (proj != null)
        {
            proj.Init(owner, speed);
        }
    }
}
