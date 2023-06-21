using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machine;

public class IdleState : StateBase
{
    private Player _player;

    public override void OnStateEnter(params object[] obj)
    {
        _player = (Player)obj[0];

        if(_player != null)
            _player.Idle();   
    }

    public override void OnStateStay()
    {
        base.OnStateStay();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
