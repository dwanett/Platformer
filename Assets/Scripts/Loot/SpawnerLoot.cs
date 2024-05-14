using System.Collections;
using UnityEngine;

public class SpawnerLoot : Spawner
{
    [SerializeField] private Loot _loot;
    [SerializeField] private float _timeRespawnDelay;
    [SerializeField] public bool _canRespawn;
    
    private Coroutine _coroutine;
    
    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        _loot.TakedLoot -= SpawnCoin;
    }
    
    private void OnEnable()
    {
        _loot.TakedLoot += SpawnCoin;
    }

    private void SpawnCoin()
    {
        _coroutine = StartCoroutine(Spawn());
    }
    
    private IEnumerator Spawn()
    {
        if (_canRespawn)
        {
            yield return new WaitForSeconds(_timeRespawnDelay);
            _loot.SetActive(true);
        }
    }
}
