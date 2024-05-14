using System;
using UnityEngine;

public abstract class CharacterMove : MonoBehaviour
{
    public event Action<bool> Moved;

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
