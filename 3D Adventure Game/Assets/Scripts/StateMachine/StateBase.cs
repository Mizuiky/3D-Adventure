using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateBase : IState
    {
        public virtual void OnStateEnter(object o = null) { }

        public virtual void OnStateStay(object o = null) { }

        public virtual void OnStateExit(object o = null) { }
    }
}

