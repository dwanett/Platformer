using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action TakedCoin;
    private void OnDisable()
    {
        TakedCoin?.Invoke();
    }
}
