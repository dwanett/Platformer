using UnityEngine;

public class PatrollingWay : MonoBehaviour
{
    private Transform[] points;

    private int _iteratorTransforms;
    
    private void Start()
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            points[i] = transform.GetChild(i);

        _iteratorTransforms = 0;
    }

    public Transform GetPositionTarget()
    {
        if (points.Length == 0)
            return transform;
        
        _iteratorTransforms++;
        
        if (_iteratorTransforms >= points.Length)
            _iteratorTransforms = 0;

        int iterator = _iteratorTransforms;
            
        return points[iterator];
    }
}
