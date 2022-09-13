using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class JumpState : StateBase
{
    private Player _player;

    public override void OnStateEnter(object o = null)
    {
        _player = (Player)o;

        if (_player != null)
        {
            _player.CanMove = false;
            _player.JumpState();
        }                  
    }

    public override void OnStateStay(object o = null)
    {
        base.OnStateStay(o);
    }

    public override void OnStateExit(object o = null)
    {
        base.OnStateExit(o);
    }
}
