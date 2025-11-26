using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    protected BossStatePattern owner;

    public virtual void Init(BossStatePattern boss)
    {
        owner = boss;
    }

    public abstract void Cast();
}
