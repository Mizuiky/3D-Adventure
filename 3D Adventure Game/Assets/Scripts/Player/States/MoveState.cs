using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class MoveState : StateBase
{
    private Player _player;

    public override void OnStateEnter(object o = null)
    {
        _player = (Player)o;

        if (_player != null)
            _player.CanMove = true;
    }

    public override void OnStateStay(object o = null)
    {
        base.OnStateStay(o);
    }

    public override void OnStateExit(object o = null)
    {
        _player.CanMove = false;
    }
}
