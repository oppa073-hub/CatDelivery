using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RunState : IPlayerState
{
    private PlayerStatePattern player;
    private Rigidbody2D rigid;
    private Vector2 moveInput;
    private float moveSpeed;
    public RunState(PlayerStatePattern Player)
    {
        player = Player;
        rigid = player.Rigidbody;
        moveInput = player.MoveInput;     
        moveSpeed = player.MoveSpeed;
    }
    public void Enter()
    {
       
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (player.MoveInput.x > 0.01f)
        {
            player.SpriteRenderer.flipX = false;
        }
        else if (player.MoveInput.x < -0.01f)
        {
            player.SpriteRenderer.flipX = true;
        }

        if (player.DashPressed)
        {
            player.SetState(new DashState(player));
            return;
        }
        // 2) 땅 위 + 점프 입력 → Jump 상태로 전환
        if (player.JumpPressed && player.JumpCount < player.MaxJumpCount)
        {
            player.SetState(new JumpState(player));
            return;
        }
        // 1) 입력이 거의 0이면 → Idle 상태로 복귀
        if (player.MoveInput.sqrMagnitude < 0.01f)
        {
            player.SetState(new IdleStat(player));
            return;
        }
       
        // 3) 실제 이동 처리: x 방향 속도만 수정 (y는 중력/점프 등 기존 값 유지)
        Vector2 velocity = rigid.linearVelocity;
        velocity.x = player.MoveInput.x * moveSpeed;
        rigid.linearVelocity = velocity;
    }
}
