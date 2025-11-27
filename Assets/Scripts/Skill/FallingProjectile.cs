using UnityEngine;

public class FallingProjectile : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float lifeTime = 3f;

    private float timer;

    public void Init(float speed)
    {
        fallSpeed = speed;
        timer = 0f;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            PoolManager.Instance.Return(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Head"))
        {
            var enemy = other.GetComponentInParent<EnemyStatePattern>();
            var boss = other.GetComponentInParent<BossStatePattern>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
            else if (boss != null)
            {
                boss.TakeDamage(1);
            }
            PoolManager.Instance.Return(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            PoolManager.Instance.Return(gameObject);
        }

    }
}

