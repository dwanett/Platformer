using System;
using System.Collections;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    private Player _target;
    
    public event Action<Player> EnterVisionPlayer;
    public event Action ExitVisionPlayer;
    
    private Coroutine _coroutine = null;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player target))
        {
            _target = target;
            EnterVisionPlayer?.Invoke(_target);
            if (_coroutine == null)
                _coroutine = StartCoroutine(Follow());
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player target))
        {
            _target = null;
            transform.right = Vector3.right;
            ExitVisionPlayer?.Invoke();
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }
    
    private IEnumerator Follow()
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        
        while (enabled)
        {
            transform.right = (_target.transform.position - transform.position) * _parent.localScale.x;
            yield return waitForFixedUpdate;
        }
    }
}
