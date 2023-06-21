using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Machine
{
    public class StateBase : IState
    {
        public virtual void OnStateEnter(params object[] obj) { }

        public virtual void OnStateStay() { }

        public virtual void OnStateExit() { }
    }
}

