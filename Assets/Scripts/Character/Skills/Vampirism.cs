using System;
using System.Collections;
using UnityEngine;

public class Vampirism : Skill
{
    [SerializeField] private Health _health;
    
    private Coroutine _coroutine = null;
    
    public override void Use()
    {
        base.Use();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _coroutine = StartCoroutine(Using());
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
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        
        while (CanUse)
        {
            Collider2D closestCollider =
                GetClosestCollider(Physics2D.OverlapCircleAll(transform.position, DistanceAttack, LayerMaskAttacked));
            
            if (closestCollider != null && closestCollider.TryGetComponent(out Character target))
            {
                if (TryAttack(target))
                    _health.AddHealth(Damage);
            }

            yield return waitForFixedUpdate;
        }
    }
}