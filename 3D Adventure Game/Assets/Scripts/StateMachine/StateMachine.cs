using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private StateBase _currentState;

    public enum States
    {
        NONE
    }

    private Dictionary<States, StateBase> gameStates;

    private void Awake()
    {
        gameStates = new Dictionary<States, StateBase>();
        gameStates.Add(States.NONE, new StateBase());

        //add switch state for the first
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_currentState != null)
            _currentState.OnStateStay();
    }

    private void StartGame()
    {
        SwitchState(States.NONE);
    }

    public void SwitchState(States state)
    {
        if (_currentState != null)
            _currentState.OnStateExit();

        _currentState = gameStates[state];
        _currentState.OnStateEnter();      
    }
}
