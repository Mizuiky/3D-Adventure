using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateMachine<T, U> where T : System.Enum where U : IGameComponent
    {
        private StateBase _currentState;
        private T _currentStateType;
        private U _gameComponent;

        public Dictionary<T, StateBase> statesDictionary;

        public T CurrentStateType
        {
            get => _currentStateType;
        }

        /* this script that is not inherited from Monobehaviour,
        shows a uncoupled state machine, any kind of game component
        can have your own state machine according with it own
        enum( that is the generic T ) */

        public StateMachine(U gameComponent)
        {
            _gameComponent = gameComponent;
        }

        public void Init()
        {
            statesDictionary = new Dictionary<T, StateBase>();
        }

        public void RegisterState(T enumType, StateBase newState)
        {
            statesDictionary.Add(enumType, newState);
        }

        public void Update()
        {
            if (_currentState != null)
                _currentState.OnStateStay();
        }

        public void SwitchState(T state)
        {
            if (_currentState != null)
                _currentState.OnStateExit();

            _currentState = statesDictionary[state];
            _currentStateType = state;

            _currentState.OnStateEnter(_gameComponent);
        }
    }
}
