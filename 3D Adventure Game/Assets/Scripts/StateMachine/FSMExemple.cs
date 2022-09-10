using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMExemple : MonoBehaviour
{
    public enum States
    {
        STATE_ONE,
        STATE_TWO
    }

    public StateMachine<States> stateMachine;

    public void Start()
    {
        Init();
        stateMachine.SwitchState(States.STATE_ONE);
    }

    private void Init()
    {
        stateMachine = new StateMachine<States>();
        stateMachine.Init();
        stateMachine.RegisterState(States.STATE_ONE, new StateBase());
    }
}
