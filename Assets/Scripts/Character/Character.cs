using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public Health Health {get; private set;}
    [field: SerializeField] public  Skill[] Skills {get; private set;}
    
    public event Action BaseAttackEvent;
    public event Action TakeDamageEvent;
    
    private void ToDie()
    {
        gameObject.SetActive(false);
    }
    
    protected void CastSkill(Skill skill)
    {
        if (skill.TryUse())
        {
            if (skill is Attack)
                BaseAttackEvent?.Invoke();
        }
    }

    protected bool TryFindSkill<T>(out T typedSkill) where T : Skill
    {
        foreach (Skill skill in Skills)
        {
            if (skill is T)
            {
                typedSkill = (T)skill;
                return true;
            }
        }

        typedSkill = null;
        return false;
    }
    
    public void TakeDamage(float damage)
    {
        Health.TakeHealth(damage);

        TakeDamageEvent?.Invoke();

        if (Health.Value <= 0f)
            ToDie();
    }
}