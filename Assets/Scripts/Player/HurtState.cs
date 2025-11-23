using UnityEngine;

public class HurtState : IPlayerState
{
    private PlayerStatePattern player;
    private Rigidbody2D rb;
    private float duration = 0.2f;
    private float timer;

    public HurtState(PlayerStatePattern Player)
    {
        player = Player;
        rb = player.Rigidbody;
    }

    public void Enter()
    {
        timer = duration;

        // 넉백 예시 (왼쪽으로 튕기게)
        Vector2 knockback = new Vector2(-1f, 1f);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockback * 5f, ForceMode2D.Impulse);

        // 애니메이션: Hurt 재생 예정 지점
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // 다시 Idle / Run으로
            if (player.MoveInput.sqrMagnitude > 0.01f)
                player.SetState(new RunState(player));
            else
                player.SetState(new IdleStat(player));
        }
    }

    public void Exit() { }
}
