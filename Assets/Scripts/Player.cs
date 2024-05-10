using UnityEngine;

public class Player : MonoBehaviour
{
    private const string Coin = "Coin";
    private int _countCoin;
    
    private void Start()
    {
        _countCoin = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Coin))
        {
            _countCoin++;
            other.gameObject.SetActive(false);
        }
    }
}
