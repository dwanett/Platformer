using UnityEngine;
using UnityEngine.UI;

public class SliderBar : ViewHealth
{
    [SerializeField] protected Slider Slider;
    
    protected override void Start()
    {
        Slider.minValue = 0;
        Slider.maxValue = Health.MaxValue;
        base.Start();
    }
    
    protected override void Change()
    {
        Slider.value = Health.Value;
    }
}
