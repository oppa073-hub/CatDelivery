using UnityEngine;

public abstract class PlayerSkillBase : MonoBehaviour
{
    protected PlayerStatePattern owner;

    public virtual void Init(PlayerStatePattern player)
    {
        owner = player;
    }

    public abstract void Cast();
}
