using UnityEngine;

public class SkillState : IPlayerState
{
    private PlayerStatePattern player;
    private float skillCooldown = 3f;   // 스킬 사용 주기
    private float skillTimer = 0f;
    private BossBallSkill skill;
    public SkillState(PlayerStatePattern Player)
    {
        player = Player;
    }

    public void Enter()
    {
        skillTimer += Time.deltaTime;
        if (skillTimer >= skillCooldown)
        {
            skillTimer = 0f; // 스킬 발동 (아직 실제 구현 X)
            Debug.Log("player Cast Skill!");
            skill.Cast(); // 0번 스킬 사용 (예시)
        }
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
       
    }
}
