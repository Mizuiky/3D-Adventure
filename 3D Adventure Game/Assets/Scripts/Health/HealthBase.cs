using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageble
{
    [SerializeField]
    private int _startLife;

    public int _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public virtual void ResetLife()
    {
        _currentLife = _startLife;
    }

    public virtual void Kill()
    {
        OnKill?.Invoke(this);
    }

    public virtual void Damage(int value)
    {
        Debug.Log("damage");

        _currentLife -= value;

        if (_currentLife <= 0)
            Kill();

        OnDamage?.Invoke(this);
    }

    public void Damage(int value, Vector3 dir)
    {
        throw new NotImplementedException();
    }
}
