using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public interface IState
    {
        public void OnStateEnter(object o = null);

        public void OnStateStay(object o = null);

        public void OnStateExit(object o = null);
    }
}

