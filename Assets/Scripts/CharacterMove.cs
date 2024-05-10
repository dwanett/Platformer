using System;
using UnityEngine;

public abstract class CharacterMove : MonoBehaviour
{
    public event Action<bool> Moved;

    protected void InvokeActionMoved(bool isMoved)
    {
        Moved?.Invoke(isMoved);
    }
}
