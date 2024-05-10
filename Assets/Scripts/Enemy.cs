using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PatrollingWay PatrollingWay { get; private set; }

    public void ReplacePatrollingWay(PatrollingWay patrollingWay)
    {
        PatrollingWay = patrollingWay;
    }

    public Transform GetNextPointTarget()
    {
       return PatrollingWay.GetPositionTarget();
    }
}
