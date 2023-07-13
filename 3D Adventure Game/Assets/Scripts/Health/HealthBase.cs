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

    public int StartLife
    {
        get { return _startLife; }
    }

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

        UpdateUi(_startLife);
    }

    public virtual void Kill()
    {
        OnKill?.Invoke(this);
    }

    public virtual void Damage(int value)
    {
        Debug.Log("damage 1");

        _currentLife -= value;

        float life = (float)_currentLife / (float)_startLife;

        UpdateUi(life);

        OnDamage?.Invoke(this);

        if (_currentLife <= 0)
            Kill();    
    }

    public void Damage(int value, Vector3 dir)
    {
        Debug.Log("damage 2");

        _currentLife -= value;

        _currentHitDirection = dir;

        float life = (float)_currentLife / (float)_startLife;

        UpdateUi(life);

        OnDamage?.Invoke(this);

        if (_currentLife <= 0)
            Kill();
    }

    private void UpdateUi(float life)
    {
        if(uiUpdater != null)
        {
            uiUpdater.ForEach(i => i.UpdateValue(life));
        }       
    }
}
