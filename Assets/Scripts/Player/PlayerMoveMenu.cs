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
}
