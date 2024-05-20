using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField, Range(0, 300f)] public float MaxValue { get; private set; }
    [field: SerializeField, Range(0, 300f)] public float Value { get; private set; }

    public event Action ChangedEvent;

    private void OnValidate()
    {
        if (Value > MaxValue)
            Value = MaxValue;
    }
    
    public void AddHealth(float health)
    {
        if (health <= 0f) 
            return;
        
        Value = Mathf.Clamp(health + Value, 0, MaxValue);
        ChangedEvent?.Invoke();
    }
    
    public void TakeHealth(float health)
    {
        if (health <= 0f) 
            return;
        
        Value = Mathf.Clamp(Value - health, 0, Value);
        ChangedEvent?.Invoke();
    }
}