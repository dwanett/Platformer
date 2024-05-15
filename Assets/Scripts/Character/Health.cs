using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Range(0, 300f)] private float _maxValue;
    [field: SerializeField, Range(0, 300f)] public float Value { get; private set; }
    
    private void OnValidate()
    {
        if (Value > _maxValue)
            Value = _maxValue;
    }
    
    public void AddHealth(float health)
    {
        if (health <= 0f) 
            return;
        
        Value = Mathf.Clamp(health + Value, 0, _maxValue);
    }
    
    public void TakeHealth(float health)
    {
        if (health <= 0f) 
            return;
        
        Value = Mathf.Clamp(Value - health, 0, Value);
    }
}