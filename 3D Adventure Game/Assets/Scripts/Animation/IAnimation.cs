using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimation
{
    public string Run { get; }
    public string Dead { get; }

    public void SetSpeed(float speed);
    public void OnRun(bool run);
    public void OnDead();
}
