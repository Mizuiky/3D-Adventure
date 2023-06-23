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

    [HideInInspector]
    public Vector3 _currentHitDirection;

    [SerializeField]
    private List<UIUpdater> uiUpdater;

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

        UpdateUi();

        OnDamage?.Invoke(this);

        if (_currentLife <= 0)
            Kill();    
    }

    public void Damage(int value, Vector3 dir)
    {
        Debug.Log("damage");

        _currentLife -= value;

        _currentHitDirection = dir;

        UpdateUi();

        OnDamage?.Invoke(this);

        if (_currentLife <= 0)
            Kill();
    }

    private void UpdateUi()
    {
        if(uiUpdater != null)
        {
            uiUpdater.ForEach(i => i.UpdateValue((float)_currentLife / _startLife));
        }       
    }
}
