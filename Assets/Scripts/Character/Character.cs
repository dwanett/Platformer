using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public Attack Attack {get; private set;}
    [field: SerializeField] public Health Health {get; private set;}
    [field: SerializeField] public Damage Damage {get; private set;}
    
    public event Action AttackEvent;
    public event Action TakeDamageEvent;
    
    public void AttackFor(Character target)
    {
        if (Attack.CanAttack)
        {
            Attack.SetCanAttack(false);
            AttackEvent?.Invoke();
            target.TakeDamage(Damage.Value);
            Attack.DelayAttack();
        }
    }

    private void TakeDamage(float damage)
    {
        Health.TakeHealth(damage);

        TakeDamageEvent?.Invoke();

        if (Health.Value <= 0f)
            ToDie();
    }
    
    private void ToDie()
    {
        gameObject.SetActive(false);
    }
}