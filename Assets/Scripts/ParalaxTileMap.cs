using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapRenderer))]
public class ParalaxTileMap : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _paralaxEffect;
    
    private float _lenght;
    private float _startpos;
    
    private void Start()
    {
        _startpos = transform.position.x;
        _lenght = 40;
    }
    
    private void FixedUpdate()
    {
        float temp = _camera.transform.position.x * (1 - _paralaxEffect);
        float dist = _camera.transform.position.x * _paralaxEffect;

        transform.position = new Vector3(_startpos + dist, transform.position.y, transform.position.z);

        if (temp > _startpos + _lenght)
            _startpos += _lenght;
        else if (temp < _startpos - _lenght)
            _startpos -= _lenght;
    }
}
