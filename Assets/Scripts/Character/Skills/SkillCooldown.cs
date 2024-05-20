using System;
using System.Collections;
using UnityEngine;

public class SkillCooldown : Skill
{
    [SerializeField] protected float _timeUse;
    [SerializeField] protected float _timeCooldown;

    private Coroutine _coroutineUse = null;

    protected bool CanUse = true;

    public event Action Enabled;
    public event Action Disabled;

    public override bool TryUse()
    {
        bool isUsing = CanUse && _coroutineUse == null;
        
        if (isUsing)
        {
            _coroutineUse = StartCoroutine(TimerUse());
            Enabled?.Invoke();
        }
        
        return isUsing;
    }

    private IEnumerator TimerUse()
    {
        yield return new WaitForSeconds(_timeUse);
        CanUse = false;
        Disabled?.Invoke();
        _coroutineUse = null;
        StartCoroutine(TimerCooldown());
    }

    private IEnumerator TimerCooldown()
    {
        yield return new WaitForSeconds(_timeCooldown);
        CanUse = true;
    }
}