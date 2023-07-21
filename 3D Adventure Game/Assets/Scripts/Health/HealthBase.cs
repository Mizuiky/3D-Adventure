using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class HealthBase : MonoBehaviour, IDamageble
{
    [SerializeField]
    protected int _startLife;

    public float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    [HideInInspector]
    public Vector3 _currentHitDirection;

    [SerializeField]
    private List<UIUpdater> uiUpdater;

    private float damageMultiplier = 1f;

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

    public virtual void Damage(float value)
    {
        Debug.Log("damage 1");

        _currentLife -= value * damageMultiplier;

        float life = (float)_currentLife / (float)_startLife;

        UpdateUi(life);

        OnDamage?.Invoke(this);

        if (_currentLife <= 0)
            Kill();    
    }

    [NaughtyAttributes.Button]
    public void KillEvent()
    {
        _currentLife = 0;
        UpdateUi(_currentLife);

        Kill();
    }

    public void Damage(float value, Vector3 dir)
    {
        Debug.Log("damage 2");

        _currentLife -= value * damageMultiplier;

        _currentHitDirection = dir;

        float life = (float)_currentLife / (float)_startLife;

        UpdateUi(life);

        OnDamage?.Invoke(this);

        if (_currentLife <= 0)
            Kill();
    }

    protected void UpdateUi(float life)
    {
        if(uiUpdater != null)
        {
            uiUpdater.ForEach(i => i.UpdateValue(life));
        }       
    }

    public void ReduceDamage(float damageMultiplier, float duration)
    {
        StartCoroutine(ReduceDamageCoroutine(damageMultiplier, duration));
    }

    public IEnumerator ReduceDamageCoroutine(float damageMultiplier, float duration)
    {
        this.damageMultiplier = damageMultiplier;

        yield return new WaitForSeconds(duration);

        this.damageMultiplier = 1;
    }
}
