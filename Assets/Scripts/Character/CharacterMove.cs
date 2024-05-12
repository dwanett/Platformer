using System;
using UnityEngine;

public abstract class CharacterMove : MonoBehaviour
{
    public event Action<bool> Moved;

    public Vector2 DirectionView()
    {
        return transform.localScale.x < 0f ? Vector2.left : Vector2.right;
    }
    
    protected void InvokeActionMoved(bool isMoved)
    {
        Moved?.Invoke(isMoved);
    }
}
