using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machine;

public class JumpState : StateBase
{
    private Player _player;

    public override void OnStateEnter(params object[] obj)
    {
        _player = (Player)obj[0];

        if (_player != null)
        {
            _player.CanMove = false;
            _player.Jump();
        }                  
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
