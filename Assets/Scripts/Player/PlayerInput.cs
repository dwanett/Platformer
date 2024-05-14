using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string FireOne = "Fire1";
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";
    
    public event Action<float> MoveEvent;
    public event Action<bool> JumpEvent;
    public event Action<bool> AssailEvent;
    
    private void Update()
    {
        MoveEvent?.Invoke(Input.GetAxis(Horizontal));
        JumpEvent?.Invoke(Input.GetButtonDown(Jump));
        AssailEvent?.Invoke(Input.GetButtonDown(FireOne));
    }
}
