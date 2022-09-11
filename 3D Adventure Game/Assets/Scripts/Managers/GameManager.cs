using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public enum GameLoop
{
    INTRO,
    GAMEPLAY,
    PAUSE,
    WIN,
    LOSE
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    //check which interface I can attribute to GameManager to create a new state machine instead using IGameComponent
    //private StateMachine<GameLoop, GameManager> _managerMachine;

    public void Update()
    {
        SwitchStateWithInput();
    }

    private void SwitchStateWithInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
            _player.SwitchPlayerState(PlayerStates.IDLE);

        else if (Input.GetKeyDown(KeyCode.M))
            _player.SwitchPlayerState(PlayerStates.MOVE);

        else if (Input.GetKeyDown(KeyCode.J))
            _player.SwitchPlayerState(PlayerStates.JUMP);
    }

    //private void Init()
    //{

    //}
}
