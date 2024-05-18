using System.Collections;
using UnityEngine;

public class SliderLerpBar : SliderBar
{
    [SerializeField] private float _speed;
    private Coroutine _coroutine;
    
    protected override void Change()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(Changer());
    }

    private IEnumerator Changer()
    {
        while (Slider.value != Health.Value)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, Health.Value, Time.deltaTime * _speed);
            yield return null;
        }
    }
}
