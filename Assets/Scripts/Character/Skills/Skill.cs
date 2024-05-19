using System;
using System.Collections;
using UnityEngine;

public abstract class Skill : Attack
{
    [SerializeField] protected float _timeUse;
    [SerializeField] protected float _timeCooldown;
    
    private Coroutine _coroutineUse = null;

    protected bool CanUse = true;

    public event Action<bool> UsingSkill;
    
    public virtual void Use()
    {
        if (CanUse && _coroutineUse == null)
        {
            UsingSkill?.Invoke(true);
            _coroutineUse = StartCoroutine(TimerUse());
        }
    }

    private IEnumerator TimerUse()
    {
        yield return new WaitForSeconds(_timeUse);
        CanUse = false;
        UsingSkill?.Invoke(false);
        _coroutineUse = null;
        StartCoroutine(TimerCooldown());
    }

    private IEnumerator TimerCooldown()
    {
        yield return new WaitForSeconds(_timeCooldown);
        CanUse = true;
    }
}