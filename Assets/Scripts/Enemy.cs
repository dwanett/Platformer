using UnityEngine;

public class Enemy : Character
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
