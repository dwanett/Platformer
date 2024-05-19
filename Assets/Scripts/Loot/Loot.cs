using System;
using UnityEngine;

public abstract class Loot : MonoBehaviour
{
    public event Action TakedLoot;
    
    public void Tacked()
    {
        TakedLoot?.Invoke();
        SetActive(false);
    }
    
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}