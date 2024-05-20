using System;
using System.Collections;
using UnityEngine;

public class Vampirism : SkillCooldown
{
    [SerializeField] private Health _health;
    [SerializeField] private float _timeDelayDammage;
    
    private Coroutine _coroutine = null;
    
    public override bool TryUse()
    {
        bool isUsing = base.TryUse();

        if (isUsing)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _coroutine = StartCoroutine(Using());
        }

        return isUsing;
    }

    private Collider2D GetClosestCollider(Collider2D[] colliders)
    {
        float maxDistance = Single.MaxValue;
        int indexСlosestCollider = 0;

        if (colliders.Length == 0)
            return null;

        for (int i = 0; i < colliders.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, colliders[i].transform.position);

            if (maxDistance > distance)
            {
                maxDistance = distance;
                indexСlosestCollider = i;
            }
        }

        return colliders[indexСlosestCollider];
    }

    private IEnumerator Using()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeDelayDammage);

        while (CanUse)
        {
            Collider2D closestCollider =
                GetClosestCollider(Physics2D.OverlapCircleAll(transform.position, DistanceUsing, LayerMaskAttacked));
            
            if (closestCollider != null && closestCollider.TryGetComponent(out Character target))
            {
                target.TakeDamage(Damage);
                _health.AddHealth(Damage);
            }

            yield return wait;
        }
    }
}