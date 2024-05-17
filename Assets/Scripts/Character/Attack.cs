using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float _timeDelayAttack;
    
    private Coroutine _delayAttack;
    
    [field: SerializeField] public float DistanceAttack { get; private set; }
    
    private void Awake()
    {
        _delayAttack = null;
    }

    private void OnValidate()
    {
        if (DistanceAttack < 0f)
            DistanceAttack = 0f;
    }

    public bool TryAttack(Character target, Damage damage)
    {
        float distanceTarget = Vector2.Distance(transform.position, target.transform.position);

        if (_delayAttack == null && distanceTarget - DistanceAttack < 0.0f)
        {
            target.TakeDamage(damage.Value);
            DelayAttack();
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
