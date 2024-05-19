using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VizualizeCircle : MonoBehaviour
{
    [SerializeField] private SkillCooldown _skillCooldown;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _countSegments;

    private Vector3 _savePostion;

    private float _pi2Deg = 360.0f;
    
    private void OnEnable()
    {
        _skillCooldown.EnabledSkill += OnDraw;
        _skillCooldown.DisabledSkill += OffDraw;
    }
    
    private void Start()
    {
        DrawCircle(Vector3.zero, _skillCooldown.DistanceUsing, _countSegments);
        _lineRenderer.enabled = false;
    }
    
    private void OnDisable()
    {
        _skillCooldown.EnabledSkill -= OnDraw;
        _skillCooldown.DisabledSkill -= OffDraw;
    }
    
    private void Update()
    {
        if (_lineRenderer.enabled && _savePostion != transform.position)
        {
            UpdatePositionCircle(transform.position, _savePostion);
            _savePostion = transform.position;
        }
    }

    private void DrawCircle(Vector3 position, float radius, int segments)
    {
        if (radius <= 0.0f || segments <= 0)
            return;
        
        float angleStep = (_pi2Deg / segments) * Mathf.Deg2Rad;
        
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
    
    private void OnDraw()
    {
        _lineRenderer.enabled = true;
    }
    
    private void OffDraw()
    {
        _lineRenderer.enabled = false;
    }
}
