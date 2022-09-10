using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    public virtual void OnStateEnter(object o = null) { }

    public virtual void OnStateStay(object o = null) { }

    public virtual void OnStateExit(object o = null) { }
}
