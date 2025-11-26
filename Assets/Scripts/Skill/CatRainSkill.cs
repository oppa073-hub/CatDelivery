using UnityEngine;
using System.Collections;
public class CatRainSkill : PlayerSkillBase
{
    [SerializeField] private int poolIndex = 1;
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private float spawnInterval = 0.2f; // 연속으로 떨어질 간격
    [SerializeField] private float fallSpeed = 6f;
    [SerializeField] private int spawnCount = 10;

    public override void Cast()
    {
        owner.StartCoroutine(SpawnRoutine());
    }
    private IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnOne();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnOne()
    {
        GameObject obj = PoolManager.Instance.Get(poolIndex);
        if (obj == null || spawnArea == null) return;

        
        Bounds bounds = spawnArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.max.y;

        // 랜덤 좌표 만들기
        Vector3 spawnPos = new Vector3(randomX, y, obj.transform.position.z);
        obj.transform.position = spawnPos;

        // 떨어지는 투사체 초기화
        var proj = obj.GetComponent<FallingProjectile>();
        if (proj != null)
        {
            proj.Init(fallSpeed);
        }
    }
}
