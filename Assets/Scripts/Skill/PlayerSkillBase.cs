using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerSkillBase : MonoBehaviour
{
    protected PlayerStatePattern owner;
    [SerializeField] protected float cooldown = 3f;  // 쿨타임 (초)
    protected float cooldownTimer = 0f;

    public bool IsOnCooldown => cooldownTimer > 0f;
    public float Cooldown => cooldown;
    public float CooldownTimer => cooldownTimer;
    public bool IsReady => cooldownTimer <= 0f;

    public virtual void Init(PlayerStatePattern player)
    {
        owner = player;
        cooldownTimer = 0f;
    }

    // 매 프레임 쿨타임 줄여주는 함수
    public virtual void Tick(float deltaTime)
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= deltaTime;
            if (cooldownTimer < 0f) cooldownTimer = 0f;
        }
    }
    public void TryCast()
    {
        Cast();
        cooldownTimer = cooldown;   
    }
    public abstract void Cast();
}
