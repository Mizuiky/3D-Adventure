using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class IdleState : StateBase
{
    private Player _player;

    public override void OnStateEnter(object o = null)
    {
        _player = (Player)o;

        if(_player != null)
            _player.Idle();   
    }

    public override void OnStateStay(object o = null)
    {
        base.OnStateStay();
    }

    public override void OnStateExit(object o = null)
    {
        base.OnStateExit();
    }
}
