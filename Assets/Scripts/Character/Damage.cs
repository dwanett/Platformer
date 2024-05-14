using UnityEngine;

public class Damage : MonoBehaviour
{
    [field: SerializeField, Range(0, 300f)] public float Value { get; private set; }
}