using System;
using System.Collections;
using UnityEngine;

public abstract class Loot : MonoBehaviour
{
    [SerializeField] protected bool _canRespawn;
    [SerializeField] protected float _timeRespawnDelay;
    
    public event Action TakedLoot;
    
    public void TackedLoot()
    {
        TakedLoot?.Invoke();
        gameObject.SetActive(false);
    }

    public IEnumerator Spawn()
    {
        if (_canRespawn)
        {
            yield return new WaitForSeconds(_timeRespawnDelay);
            gameObject.SetActive(true);
        }
    }
}