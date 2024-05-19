using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(PlayerInput), typeof(PlayerMove))]
public class Player : Character
{
    [SerializeField] private PlayerInput _playerInput;
    
    private int _countCoin;
    
    public event Action Die;
    
    private void Start()
    {
        _countCoin = 0;
    }

    private void OnEnable()
    {
        _playerInput.AttackEvent += BaseAttack;
        _playerInput.AttackVampirism += VampirismAttack;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Loot loot))
        {
            if (loot is Coin)
                _countCoin++;
            else if (loot is KitHealth kitHealth)
                Health.AddHealth(kitHealth.CountAddHealth);

            loot.Tacked();
        }
    }
    
    private void OnDisable()
    {
        _playerInput.AttackEvent -= BaseAttack;
        _playerInput.AttackVampirism -= VampirismAttack;
        Die?.Invoke();
    }
    
    private void VampirismAttack(bool isAttack)
    {
        Attack<Vampirism>(isAttack);
    }
    
    private void BaseAttack(bool isAttack)
    {
        Attack<Attack>(isAttack);
    }
    
    private void Attack<T>(bool isAttack) where T : Skill
    {
        if (isAttack && TryFindSkill(out T attack))
            CastSkill(attack);
    }
}
