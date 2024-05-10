using UnityEngine;

public class PatrollingWay : MonoBehaviour
{
    private Transform[] points;

    private int iteratorTransforms;
    private void Start()
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            points[i] = transform.GetChild(i);

        iteratorTransforms = 0;
    }

    public Transform GetPositionTarget()
    {
        if (points.Length == 0)
            return transform;
        
        iteratorTransforms++;
        
        if (iteratorTransforms >= points.Length)
            iteratorTransforms = 0;

        int iterator = iteratorTransforms;
            
        return points[iterator];
    }
}
