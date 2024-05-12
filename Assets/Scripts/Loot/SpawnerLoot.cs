using UnityEngine;

public class SpawnerLoot : MonoBehaviour
{
    [SerializeField] private Loot _loot;

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
        _coroutine = StartCoroutine(_loot.Spawn());
    }
}
