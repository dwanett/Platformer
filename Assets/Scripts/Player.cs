using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [SerializeField] private Collider2D _collider2D;

    public float DeltaTransformCollider => _collider2D.bounds.size.x;
    
    private int _countCoin;
    
    private void Start()
    {
        _countCoin = 0;
    }

    private void OnDisable()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Loot loot))
        {
            if (loot is Coin)
                _countCoin++;
            else if (loot is KitHealth kitHealth)
                AddHealth(kitHealth.CountAddHealth);

            loot.TackedLoot();
        }
    }
}
