using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public Attack Attacker {get; private set;}
    [field: SerializeField] public Health Health {get; private set;}
    [field: SerializeField] public Skill Skiller {get; private set;}
    
    public event Action AttackEvent;
    public event Action TakeDamageEvent;
    
    private void ToDie()
    {
        gameObject.SetActive(false);
    }
    
    protected void Attack(Character target)
    {
        if (Attacker.IsDistanceReached(target) && Attacker.TryAttack(target))
            AttackEvent?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        Health.TakeHealth(damage);

        TakeDamageEvent?.Invoke();

        if (Health.Value <= 0f)
            ToDie();
    }
}