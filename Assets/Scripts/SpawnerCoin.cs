using System.Collections;
using UnityEngine;

public class SpawnerCoin : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    
    private void OnDisable()
    {
        _coin.TakedCoin -= SpawnCoin;
    }
    
    private void OnEnable()
    {
        _coin.TakedCoin += SpawnCoin;
    }

    private void SpawnCoin()
    {
        StartCoroutine(Spawn());
    }
    
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(10);
        _coin.gameObject.SetActive(true);
    }
}
