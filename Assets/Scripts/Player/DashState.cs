using UnityEngine;

public class DashState : IPlayerState
{
    private PlayerStatePattern player;
    private Rigidbody2D rigid;
    private float dashSpeed;
    private float dir;
    private float dashTime = 0.28f;
    private float timer;
    public DashState(PlayerStatePattern Player)
    {
        player = Player;
        rigid = player.Rigidbody;
        dashSpeed = player.DashSpeed;
       
    }

    public void Enter()
    {
        timer = dashTime;

        dir = player.MoveInput.x != 0 ? Mathf.Sign(player.MoveInput.x) : 1f;

        Vector2 velocity = rigid.linearVelocity;
        velocity.x = dir * dashSpeed;
        rigid.linearVelocity = velocity;
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        rigid.linearVelocity = new Vector2(dir * dashSpeed, rigid.linearVelocity.y);

        if (timer <= 0f)
        {
            if (player.MoveInput.sqrMagnitude > 0.01f)
                player.SetState(new RunState(player));
            else
                player.SetState(new IdleStat(player));
        }
    }
}
