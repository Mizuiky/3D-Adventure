using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBase : MonoBehaviour, IAnimation
{
    protected Animator _animator;

    [Header("Animation Parameters")]

    [SerializeField]
    private string _run;
    [SerializeField]
    private string _dead;

    public string Run
    {
        get => _run;
    }

    public string Dead
    {
        get => _dead;
    }

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void OnIdle()
    {
        if(_animator.GetBool(_run))
            _animator.SetBool(_run, false);
    }

    public virtual void OnRun()
    {
        _animator.SetBool(_run, true);
    }

    public virtual void OnDead()
    {
        _animator.SetBool(_dead, true);
    }
}
