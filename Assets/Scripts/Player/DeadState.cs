using UnityEngine;

public class DeadState : IPlayerState
{
    private PlayerStatePattern player;
    private Rigidbody2D rb;

    public DeadState(PlayerStatePattern Player)
    {
        player = Player;
        rb = player.Rigidbody;
    }

    public void Enter()
    {
        // 죽는 모션, 애니메이션, 리지드바디 자유낙하 등
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;   // 물리 멈추거나, 반대로 ragdoll처럼 놔두거나

      
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
       
    }
}
