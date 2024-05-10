using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action TakedCoin;
    
    public void TackedCoin()
    {
        TakedCoin?.Invoke();
        gameObject.SetActive(false);
    }
}
