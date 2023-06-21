using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machine;
using DG.Tweening;

namespace Boss
{
    public class BossBase : MonoBehaviour, IGameComponent
    {
        public enum BossStates
        {
            INIT,
            IDLE,
            WALK,
            ATTACK,
            DEATH
        }

        [Header("StartAnimation")]
        public float startAnimationDuration = .5f;
        public Ease startEase = Ease.OutBack;

        [Header("Movement Points")]
        public Transform[] points;
        public float speed;
        private int index;

        protected bool isAlive = false;

        public StateMachine<BossStates, BossBase> stateMachine;

        public void Awake()
        {
            Init();
        }

        public virtual void Init()
        {
            Activate();

            stateMachine.RegisterState(BossStates.INIT, new BossStateInit());
            stateMachine.RegisterState(BossStates.IDLE, new BossStateIdle());
            stateMachine.RegisterState(BossStates.WALK, new BossStateWalk());
            stateMachine.RegisterState(BossStates.ATTACK, new BossStateAttack());
            stateMachine.RegisterState(BossStates.DEATH, new BossStateDeath());
        }

        public void Activate()
        {
            stateMachine = new StateMachine<BossStates, BossBase>(this);
            stateMachine.Init();

            isAlive = true;

            transform.DOScale(1, startAnimationDuration).SetEase(startEase).From();
        }

        public void Deactivate()
        {
            
        }

        #region Movement

        public void WalkThroughPoints()
        {
            Debug.Log("will walk");

            StartCoroutine(Walk());
        }

        public IEnumerator Walk()
        {
            transform.DOScaleY(2f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

            while (isAlive)
            {
                

                if (Vector3.Distance(transform.position, points[index].position) < 1f)
                {
                    index++;
                    if(index >= points.Length)
                    {
                        index = 0;
                    }
                }

                transform.position = Vector3.MoveTowards(transform.position, points[index].position, Time.deltaTime * speed);

                yield return new WaitForEndOfFrame();
            }
        }

        #endregion
        [NaughtyAttributes.Button]
        public void SwitchInitState()
        {
            stateMachine.SwitchState(BossStates.INIT, this);
        }

        [NaughtyAttributes.Button]
        public void SwitchWalkState()
        {
            stateMachine.SwitchState(BossStates.WALK, this);
        }
    }
}

