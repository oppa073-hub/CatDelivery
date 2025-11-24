using NUnit.Framework.Interfaces;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class IdleStat : IPlayerState
{
    private PlayerStatePattern player;

    public IdleStat(PlayerStatePattern Player)
    {
        player = Player;
    }

    public void Enter()
    {
        Vector2 v = player.Rigidbody.linearVelocity;
        v.x = 0f;
        player.Rigidbody.linearVelocity = v;
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
        if (player.JumpPressed && player.JumpCount < player.MaxJumpCount)
        {
            player.SetState(new JumpState(player));
            return;
        }

        // 2) 이동 입력이 조금이라도 있다면 → Run 상태로 전환
        if (player.MoveInput.sqrMagnitude >= 0.01f)
        {
            player.SetState(new RunState(player));
            return;
        }
      
    }
}
