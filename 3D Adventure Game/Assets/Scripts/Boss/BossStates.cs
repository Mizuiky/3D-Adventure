using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machine;
using DG.Tweening;
using System;

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

            boss.StartBossAnimation(OnStart);
        }

        public void OnStart()
        {
            boss.stateMachine.SwitchState(BossBase.BossStates.WALK);
        }
    }

    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] obj)
        {
            Debug.Log("WALK STATE");
            base.OnStateEnter(obj);
            boss.WalkThroughPoints(OnArrive);
        }

        public void OnArrive()
        {
            boss.stateMachine.SwitchState(BossBase.BossStates.ATTACK);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
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

    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] obj)
        {
            Debug.Log("ATTACK STATE");
            base.OnStateEnter(obj);
            boss.StartAttack(EndAttack);
        }

        public void EndAttack()
        {
            boss.stateMachine.SwitchState(BossBase.BossStates.WALK);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }

    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] obj)
        {
            base.OnStateEnter(obj);
            Debug.Log("DEATH STATE");

            boss.transform.localScale = Vector3.one * 0.3f;
            boss.StopAllCoroutines();
            boss._currentWalkTween.Kill();
        }
    }
}
