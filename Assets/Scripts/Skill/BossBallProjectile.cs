using UnityEngine;

public class BossBallProjectile : MonoBehaviour
{
    public int damage = 2;  // 플레이어에게 줄 데미지
    [SerializeField] private float lifeTime = 3f;

    private float speed;
    private Vector2 direction;
    private float timer;

    public void Init(Vector2 dir, float Speed)
    {
        direction = dir.normalized;
        speed = Speed;
        timer = 0f;

       
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            PoolManager.Instance.Return(gameObject);
        }
    }
}
