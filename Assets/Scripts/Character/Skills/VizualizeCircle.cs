using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VizualizeCircle : MonoBehaviour
{
    [SerializeField] private Skill _skiller;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _countSegments;

    private Vector3 savePostion;
    private void OnEnable()
    {
        _skiller.UsingSkill += Draw;
    }
    
    private void Start()
    {
        DrawCircle(Vector3.zero, _skiller.DistanceAttack, _countSegments);
        _lineRenderer.enabled = false;
    }
    
    private void OnDisable()
    {
        _skiller.UsingSkill -= Draw;
    }
    
    private void Update()
    {
        if (_lineRenderer.enabled && savePostion != transform.position)
        {
            UpdatePositionCircle(transform.position, savePostion);
            savePostion = transform.position;
        }
    }

    private void DrawCircle(Vector3 position, float radius, int segments)
    {
        if (radius <= 0.0f || segments <= 0)
            return;
        
        float angleStep = (360.0f / segments) * Mathf.Deg2Rad;
        
        Vector3 line = Vector3.zero;
 
        for (int i = 0; i < segments; i++)
        {
            line.x = Mathf.Cos(angleStep * i);
            line.y = Mathf.Sin(angleStep * i);
            
            line = line * radius + position;

            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(i, line);
        }
    }

    private void UpdatePositionCircle(Vector3 newPosition, Vector3 oldPosition)
    {
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            _lineRenderer.SetPosition(i,  _lineRenderer.GetPosition(i) + newPosition - oldPosition);
        }
    }
    
    private void Draw(bool canDraw)
    {
        _lineRenderer.enabled = canDraw;
    }
}
