using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMoveMenu : CharacterMove
{
    
    [SerializeField] private float _speed;
    [SerializeField, Range(-1, 1)] private float _directionMove;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        if (_directionMove != 0f)
        {
            InvokeActionMoved(true);
            Flip(_directionMove);
        }
        else
        {
            InvokeActionMoved(false);
        }
        _rigidbody.velocity = new Vector2(_directionMove * _speed, _rigidbody.velocity.y);
    }
    
    private void Flip(float horizontalAxis)
    {
        Vector3 localScale = transform.localScale;
        
        if (Mathf.Clamp(horizontalAxis / Mathf.Abs(horizontalAxis), -1, 1) != Mathf.Clamp(localScale.x, -1, 1))
            localScale.x *= -1;
        
        transform.localScale = localScale;
    }
}
