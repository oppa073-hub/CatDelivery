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

        // 입력도 더이상 안 받게 하고 싶으면
        // PlayerStatePattern에서 GameState.GameOver면 Update() 자체 Return하게 되어 있으니 그걸로도 충분
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
       
    }
}
