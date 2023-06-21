using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machine;

public class FSMExemple : MonoBehaviour, IGameComponent
{
    public enum States
    {
        STATE_ONE,
        STATE_TWO
    }

    public StateMachine<States, FSMExemple> stateMachine;

    public void Start()
    {
        Init();
        stateMachine.SwitchState(States.STATE_ONE);
    }

    private void Init()
    {
        stateMachine = new StateMachine<States, FSMExemple>(this);
        stateMachine.Init();
        stateMachine.RegisterState(States.STATE_ONE, new StateBase());
    }

    public void Activate()
    {
        throw new System.NotImplementedException();
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }
}
