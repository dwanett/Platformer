using System;
using System.Collections;
using UnityEngine;

public abstract class Loot : MonoBehaviour
{
    [SerializeField] private bool _canRespawn;
    [SerializeField] private float _timeRespawnDelay;
    
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