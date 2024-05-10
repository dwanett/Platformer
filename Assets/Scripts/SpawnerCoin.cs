using System.Collections;
using UnityEngine;

public class SpawnerCoin : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    private Coroutine _coroutine;
    
    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        _coin.TakedCoin -= SpawnCoin;
    }
    
    private void OnEnable()
    {
        _coin.TakedCoin += SpawnCoin;
    }

    private void SpawnCoin()
    {
        _coroutine = StartCoroutine(Spawn());
    }
    
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(10);
        _coin.gameObject.SetActive(true);
    }
}
