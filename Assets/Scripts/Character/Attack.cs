using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float _timeDelayAttack;
    [field: SerializeField, Range(0, 300f)] public float Damage {get; private set;}
    [field: SerializeField] public LayerMask LayerMaskAttacked {get; private set;}
    [field: SerializeField] public float DistanceAttack { get; private set; }
    
    private Coroutine _delayAttack;

    private void Awake()
    {
        _delayAttack = null;
    }

    private void OnValidate()
    {
        if (DistanceAttack < 0f)
            DistanceAttack = 0f;
    }

    public bool IsDistanceReached(Character target)
    {
        float distanceTarget = Vector2.Distance(transform.position, target.transform.position);

        return distanceTarget - DistanceAttack < 0.0f;
    }
    
    public bool TryAttack(Character target)
    {
        if (_delayAttack == null)
        {
            DelayAttack();
            target.TakeDamage(Damage);
            return true;
        }
        
        return false;
    }
    
    public void DelayAttack()
    {
        if (_delayAttack == null)
            _delayAttack = StartCoroutine(Delay());
    }
    
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_timeDelayAttack);
        _delayAttack = null;
    }
}
