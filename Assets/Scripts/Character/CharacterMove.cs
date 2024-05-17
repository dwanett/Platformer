using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class CharacterMove : MonoBehaviour
{
    [SerializeField] protected LayerMask FloorMask;
    [SerializeField] protected SpriteRenderer SpriteRenderer;
    protected bool OnGround;
    public event Action<bool> Moved;

    private void Start()
    {
        OnGround = true;
    }
    
    public Vector2 GetDirectionView()
    {
        return transform.localScale.x < 0f ? Vector2.left : Vector2.right;
    }
    
    protected void InvokeActionMoved(bool isMoved)
    {
        Moved?.Invoke(isMoved);
    }
    
    protected void Flip(float horizontalAxis)
    {
        Vector3 localScale = transform.localScale;
        
        if (Mathf.Clamp(horizontalAxis / Mathf.Abs(horizontalAxis), -1, 1) != Mathf.Clamp(localScale.x, -1, 1))
            localScale.x *= -1;
        
        transform.localScale = localScale;
    }
}
