using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object [] obj) 
        {
            boss = (BossBase)(obj[0]);
        }

        public override void OnStateStay() 
        {
            
        }

        public override void OnStateExit() 
        {
            
        }
    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] obj)
        {
            base.OnStateEnter(obj);
            Debug.Log("boss name" + boss.name);
        }
    }

    public class BossStateIdle : BossStateBase
    {
        public override void OnStateEnter(params object[] obj)
        {
            base.OnStateEnter(obj);
            Debug.Log("IDLE STATE");
        }
    }

    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] obj)
        {
            base.OnStateEnter(obj);
            boss.WalkThroughPoints();
        }
    }

    public class BossStateAttack : BossStateBase
    {

    }

    public class BossStateDeath : BossStateBase
    {

    }

}
