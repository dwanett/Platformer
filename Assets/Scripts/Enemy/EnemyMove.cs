using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(Rigidbody2D))]
public class EnemyMove : CharacterMove
{
    [SerializeField] private LayerMask _layerMaskPlayer;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _distanceVision;
    [SerializeField] private float _speed;

    private Player _targetPlayer;
    private Transform _targetTransform;
    
    private void Start()
    {
        _targetPlayer = null;
        _targetTransform = _enemy.GetNextPointTarget();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _targetTransform && _targetPlayer is null)
            _targetTransform = _enemy.GetNextPointTarget();
    }

    private void FixedUpdate()
    {
        MoveToTarget();

        if (Physics2D.Raycast(transform.position, Vector2.down,
                SpriteRenderer.bounds.extents.y + 0.1f, FloorMask.value))
            OnGround = true;
        else
            OnGround = false;
        
        if (_targetPlayer is not null)
            ChasePlayer();
        else
            Vision();
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _targetTransform.position);
        
        if (distanceToPlayer < _distanceVision + _targetPlayer.DeltaTransformCollider)
        {
            if (distanceToPlayer - _enemy.Attack.DistanceAttack < 0.0f)
            {
                _enemy.AttackFor(_targetPlayer);
            }
        }
        else
        {
            _targetPlayer = null;
            _targetTransform = _enemy.GetNextPointTarget();
        }
    }
    
    private void Vision()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, GetDirectionView(), _distanceVision, _layerMaskPlayer.value);
        
        if (raycastHit2D && raycastHit2D.collider.TryGetComponent(out Player targetPlayer))
        {
            _targetPlayer = targetPlayer;
            _targetTransform = _targetPlayer.transform;
        }
    }
    
    private void MoveToTarget()
    {
        Vector3 direction = _targetTransform.position - transform.position;
        direction.z = 0;
        
        if (direction.normalized.x != 0f)
        {
            Flip(direction.normalized.x);
            InvokeActionMoved(OnGround);
        }
        else
        {  
            InvokeActionMoved(false);
        }

        _rigidbody2D.velocity = direction.normalized * _speed;
    }
}
