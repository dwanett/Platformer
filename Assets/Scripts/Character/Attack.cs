using UnityEngine;

public class Attack : Skill
{
    public override bool TryUse()
    {
        bool isUsing = false;
        
        Vector2 directionView = transform.localScale.x < 0f ? Vector2.left : Vector2.right;
        
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, directionView,
            DistanceUsing, LayerMaskAttacked.value);

        if (raycastHit2D && raycastHit2D.collider.TryGetComponent(out Character target))
        {
            isUsing = IsDistanceReached(target) && base.TryUse();
            
            if (isUsing)
                target.TakeDamage(Damage);
        }
        
        return isUsing;
    }
}
