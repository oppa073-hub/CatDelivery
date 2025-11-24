using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
        if (player.JumpCount < player.MaxJumpCount)
        {
            Vector2 velocity = rigid.linearVelocity;
        velocity.y = jumpForce;
        rigid.linearVelocity = velocity;
            player.AddJumpCount();
        }
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
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
}
