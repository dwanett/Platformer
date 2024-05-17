using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(PlayerInput), typeof(PlayerMove))]
public class Player : Character
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private LayerMask _layerMaskAttacked;
    
    private int _countCoin;
    
    public event Action Die;
    
    private void Start()
    {
        _countCoin = 0;
    }

    private void OnEnable()
    {
        _playerInput.AssailEvent += Assail;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Loot loot))
        {
            if (loot is Coin)
                _countCoin++;
            else if (loot is KitHealth kitHealth)
                Health.AddHealth(kitHealth.CountAddHealth);

            loot.TackedLoot();
        }
    }
    
    private void OnDisable()
    {
        _playerInput.AssailEvent -= Assail;
        Die?.Invoke();
    }
    
    private void Assail(bool isAttack)
    {
        if (isAttack)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _playerMove.GetDirectionView(),
                Attacker.DistanceAttack, _layerMaskAttacked.value);

            if (raycastHit2D && raycastHit2D.collider.TryGetComponent(out Enemy targetEnemy))
                Attack(targetEnemy);
        }
    }
}
