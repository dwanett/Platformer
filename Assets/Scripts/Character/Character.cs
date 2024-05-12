using System;
using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField, Range(0, 300f)] protected float _maxHealth;
    [SerializeField, Range(0, 300f)] protected float _health;
    [SerializeField, Range(0, 300f)] protected float _damage;
    [SerializeField] protected float _timeDelayAttack;
    
    public event Action AttackEvent;
    public event Action TakeDamageEvent;

    private Coroutine _coroutine;
    
    [field: SerializeField] public float DistanceAttack { get; private set; }

    public bool CanAttack { get; private set; }

    private void Awake()
    {
        CanAttack = true;
    }

    private void OnValidate()
    {
        if (_health > _maxHealth)
            _health = _maxHealth;
        
        if (DistanceAttack < 0f)
            DistanceAttack = 0f;
    }
    
    public void Attack(Character target)
    {
        if (CanAttack)
        {
            CanAttack = false;
            AttackEventInvoke();
            target.TakeDamage(_damage);
            
            if (_coroutine == null)
                _coroutine = StartCoroutine(DelayAttack());
        }
    }
    
    public void AttackEventInvoke()
    {
        AttackEvent?.Invoke();
    }
    
    protected void AddHealth(float health)
    {
        if (_health + health > _maxHealth)
            _health = _maxHealth;
        else
            _health += health;
        
        Debug.Log($"СЮДА АПТЕЧКУ!!!! ТЕПРЬ У МЕНЯ ДОХУЯ ЖИЗНЕЙ {_health}");
    }

    private void TakeDamage(float damage)
    {
        Debug.Log($"{this} БЫЛО: {_health} ОСТАЛОСЬ: {_health - damage}");
        _health -= damage;
        
        TakeDamageEvent?.Invoke();
        
        if (_health <= 0f)
        {
            _health = 0f;
            ToDie();
        }
    }
    
    private void ToDie()
    {
        gameObject.SetActive(false);
    }
    
    private IEnumerator DelayAttack()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeDelayAttack);
        
        while (enabled)
        {
            yield return waitForSeconds;
            CanAttack = true;
        }
    }
}