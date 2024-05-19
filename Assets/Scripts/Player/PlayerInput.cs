using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string FireOne = "Fire1";
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";
    private const KeyCode UseSkill = KeyCode.E;
    
    public event Action<float> MoveEvent;
    public event Action<bool> JumpEvent;
    public event Action<bool> AttackEvent;
    public event Action<bool> AttackVampirism;
    
    private void Update()
    {
        MoveEvent?.Invoke(Input.GetAxis(Horizontal));
        JumpEvent?.Invoke(Input.GetButton(Jump));
        AttackEvent?.Invoke(Input.GetButton(FireOne));
        AttackVampirism?.Invoke(Input.GetKey(UseSkill));
    }
}
