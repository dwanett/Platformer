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
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeRespawnDelay);
        
        if (_canRespawn)
        {
            yield return waitForSeconds;
            gameObject.SetActive(true);
        }
    }
}