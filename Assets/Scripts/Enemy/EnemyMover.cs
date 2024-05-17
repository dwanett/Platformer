using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(Rigidbody2D))]
public class EnemyMover : CharacterMove
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Vision _vision;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;

    private Transform _targetTransform;
    
    private void Start()
    {
        _targetTransform = _enemy.GetNextPointTarget();
        OnGround = true;
    }

    private void OnEnable()
    {
        _vision.EnterVisionPlayer += SetTargetMovePlayer;
        _vision.ExitVisionPlayer += ResetTargetMovePlayer;
    }

    private void OnDisable()
    {
        _vision.EnterVisionPlayer -= SetTargetMovePlayer;
        _vision.ExitVisionPlayer -= ResetTargetMovePlayer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _targetTransform)
            _targetTransform = _enemy.GetNextPointTarget();
    }
    
    private void FixedUpdate()
    {
        MoveToTarget();
        
        OnGround = Physics2D.Raycast(transform.position, Vector2.down, 
            SpriteRenderer.bounds.extents.y + 0.1f, FloorMask.value);
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
    
    private void SetTargetMovePlayer(Player player)
    {
        _targetTransform = player.transform;
    }
    
    private void ResetTargetMovePlayer()
    {
        _targetTransform = _enemy.GetNextPointTarget();
    }
}
