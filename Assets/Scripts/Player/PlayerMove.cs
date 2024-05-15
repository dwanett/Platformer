using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : CharacterMove
{
    [Header("Move"), Space]
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    
    [Header("Camera"), Space]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speedCamera;
    
    private bool _isMoved;
    private Coroutine _moving;
    private float _horizontalAxis;
    
    private void OnEnable()
    {
        _playerInput.MoveEvent += Move;
        _playerInput.JumpEvent += Jump;
    }

    private void OnDisable()
    {
        _playerInput.MoveEvent -= Move;
        _playerInput.JumpEvent -= Jump;
    }

    private void Start()
    {
        StartCoroutine(MoveCamera());
        _horizontalAxis = 0;
        _isMoved = false;
        OnGround = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position, Vector2.down,
                SpriteRenderer.bounds.extents.y + 0.1f, FloorMask.value))
            OnGround = true;
        else
            OnGround = false;
    }

    private IEnumerator MoveCamera()
    {
        while (enabled)
        {
            Vector3 offsetPositionCamera = new Vector3(transform.position.x, transform.position.y, _camera.transform.position.z);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, offsetPositionCamera, Time.deltaTime * _speedCamera);
            yield return null;
        }
    }

    private void Jump(bool isJumped)
    {
        if (OnGround && isJumped)
        {
            OnGround = false;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _forceJump);
        }
    }
    
    private void Move(float horizontalAxis)
    {
        _horizontalAxis = horizontalAxis;
        
        if (_isMoved == false && _horizontalAxis != 0f)
        {
            if (_moving != null)
                StopCoroutine(_moving);

            _isMoved = true;
            Flip(_horizontalAxis);
            _moving = StartCoroutine(Moving());
        }
        else if (_horizontalAxis == 0f)
        {
            _isMoved = false;
        }
    }
    
    private IEnumerator Moving()
    {
        while (enabled)
        {
            InvokeActionMoved(_isMoved && OnGround);
            _rigidbody.velocity = new Vector2(_horizontalAxis * _speed, _rigidbody.velocity.y);
            yield return new WaitForFixedUpdate();
        }
    }
}
