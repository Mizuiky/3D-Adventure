using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Machine
{
    public interface IState
    {
        public void OnStateEnter(params object[] obj);

        public void OnStateStay();

        public void OnStateExit();
    }
}

