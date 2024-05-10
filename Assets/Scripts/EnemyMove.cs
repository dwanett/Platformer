using System;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;

    private bool checkPoint;
    private Transform targetTransform;
    
    public event Action<bool> Moved;
    
    private void Start()
    {
        checkPoint = false;
        targetTransform = _enemy.GetNextPointTarget();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == targetTransform)
            targetTransform = _enemy.GetNextPointTarget();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 direction = targetTransform.position - transform.position;
        direction.z = 0;

        if (direction.normalized.x != 0)
        {
            Moved?.Invoke(true);
            Flip(direction.normalized.x);
        }
        else
        {
            Moved?.Invoke(false);
        }
        
        _rigidbody2D.velocity = direction.normalized * _speed;
    }
    
    private void Flip(float horizontalAxis)
    {
        Vector3 localScale = transform.localScale;
        
        if (Mathf.Clamp(horizontalAxis / Mathf.Abs(horizontalAxis), -1, 1) != Mathf.Clamp(localScale.x, -1, 1))
            localScale.x *= -1;
        
        transform.localScale = localScale;
    }
}
