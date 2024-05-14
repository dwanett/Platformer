using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float _timeDelayAttack;
    
    [field: SerializeField] public float DistanceAttack { get; private set; }
    public bool CanAttack { get; private set; }
    
    private Coroutine _coroutine;
    
    private void Awake()
    {
        CanAttack = true;
        _coroutine = null;
    }

    private void OnValidate()
    {
        if (DistanceAttack < 0f)
            DistanceAttack = 0f;
    }

    public void SetCanAttack(bool canAttack)
    {
        CanAttack = canAttack;
    }
    
    public void DelayAttack()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Delay());
    }
    
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_timeDelayAttack);
        CanAttack = true;
        _coroutine = null;
    }
}
