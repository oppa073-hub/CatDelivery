using UnityEngine;


public class JumpState : IPlayerState
{
    private PlayerStatePattern player;
    private Rigidbody2D rigid;
    private float jumpForce;

    public JumpState(PlayerStatePattern Player)
    {
        player = Player;
        rigid = player.Rigidbody;
        jumpForce = player.JumpForce;
    }

    public void Enter()
    {
        TryJump();
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        Vector2 velocity = rigid.linearVelocity;
        velocity.x = player.MoveInput.x * player.MoveSpeed;
        rigid.linearVelocity = velocity;

        if (player.MoveInput.x > 0.01f)
            player.SpriteRenderer.flipX = false;
        else if (player.MoveInput.x < -0.01f)
            player.SpriteRenderer.flipX = true;

        if (player.JumpPressed)
        {
            TryJump();   // 남은 점프 횟수 있으면 한 번 더 점프
        }
        if (player.DashPressed)
        {
            player.SetState(new DashState(player));
            return;
        }
        if (!player.IsGround)
        {
            return;
        }
        if (player.IsGround)
        {
          
            if (player.MoveInput.sqrMagnitude < 0.01f)
            {
                player.SetState(new IdleStat(player));
            }
            else
            {
                player.SetState(new RunState(player));
            }
        }
    }
    private void TryJump()
    {
        if (player.JumpCount >= player.MaxJumpCount)
            return;

        Vector2 v = rigid.linearVelocity;
        v.y = jumpForce;
        rigid.linearVelocity = v;

        player.AddJumpCount();
     
    }
}
