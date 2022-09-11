using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimation
{
    public string Run { get; }
    public string Dead { get; }

    public void OnIdle();
    public void OnRun();
    public void OnDead();
}
