using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string FireOne = "Fire1";
    
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private LayerMask _layerMaskAttacked;

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetButtonDown(FireOne))
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _playerMove.DirectionView(), _player.DistanceAttack, _layerMaskAttacked.value);
            
            if (raycastHit2D && raycastHit2D.collider.TryGetComponent(out Enemy targetEnemy))
                _player.Attack(targetEnemy);
        }
    }
}
