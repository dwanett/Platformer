using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMove : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";
    private const string NameMaskFloor = "Floor";
    
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Camera _camera;
    [SerializeField] private float _speedCamera;
    
    private bool _isJumped;
    private int _numberMaskFloor;

    public event Action<bool> Moved;
    
    private void Start()
    {
        StartCoroutine(MoveCamera());
        _numberMaskFloor = LayerMask.NameToLayer(NameMaskFloor);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _numberMaskFloor && Physics2D.Raycast(transform.position, Vector2.down,
                _spriteRenderer.bounds.extents.y + 0.1f, LayerMask.GetMask(NameMaskFloor)))
        {
            _isJumped = false;
        }
    }
    
    private void FixedUpdate()
    {
        Move();
        Jumped();
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
    
    private void Jumped()
    {
        float jumpAxis = Input.GetAxis(Jump);
        
        if (jumpAxis != 0f)
        {
            if (_isJumped == false)
            {
                _isJumped = true;
                Moved?.Invoke(false);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _forceJump);
            }
        }
    }
    
    private void Move()
    {
        float horizontalAxis = Input.GetAxis(Horizontal);

        if (horizontalAxis != 0f)
        {
            Moved?.Invoke(_isJumped == false);
            Flip(horizontalAxis);
        }
        else
        {
            Moved?.Invoke(false);
        }

        _rigidbody.velocity = new Vector2(horizontalAxis * _speed, _rigidbody.velocity.y);
    }
    
    private void Flip(float horizontalAxis)
    {
        Vector3 localScale = transform.localScale;
        
        if (Mathf.Clamp(horizontalAxis / Mathf.Abs(horizontalAxis), -1, 1) != Mathf.Clamp(localScale.x, -1, 1))
            localScale.x *= -1;
        
        transform.localScale = localScale;
    }
}
