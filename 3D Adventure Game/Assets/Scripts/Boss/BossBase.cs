using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machine;
using DG.Tweening;
using System.Linq;
using System;

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

        [Header("Movement WayPoints")]
        public Transform[] points;
        public float speed;
        private int _index;
        public Tween _currentWalkTween;
        private float _yScale;
        private float _startPositionY;

        [Header("Attack")]
        public int attackAmount;
        public float timeBetweenAttack;
        private List<int> _randomPoints;
        private int _lastRandomPoint;
        private int _randomPoint;

        protected bool isAlive = false;

        public HealthBase healthBase;

        public StateMachine<BossStates, BossBase> stateMachine;

        public void Awake()
        {
            Init();

            healthBase.OnKill += OnBossKill;
        }

        public virtual void Init()
        {
            Activate();

            stateMachine.RegisterState(BossStates.INIT, new BossStateInit());
            stateMachine.RegisterState(BossStates.IDLE, new BossStateIdle());
            stateMachine.RegisterState(BossStates.WALK, new BossStateWalk());
            stateMachine.RegisterState(BossStates.ATTACK, new BossStateAttack());
            stateMachine.RegisterState(BossStates.DEATH, new BossStateDeath());

            stateMachine.SwitchState(BossStates.INIT);
        }

        public void Activate()
        {
            stateMachine = new StateMachine<BossStates, BossBase>(this);
            stateMachine.Init();

            _randomPoints = new List<int>();

            for (int i = 0; i < points.Length; i++)
            {
                _randomPoints.Add(i);
            }

            _lastRandomPoint = _randomPoint = 0;

            _startPositionY = transform.position.y;

            _yScale = transform.localScale.y;

            isAlive = true;
        }

        public void Deactivate()
        {

        }

        #region Start Animation

        public void StartBossAnimation(Action onStart = null)
        {
            StartCoroutine(StartAnimation(onStart));
        }

        public IEnumerator StartAnimation(Action onStart = null)
        {
            transform.DOScale(.5f, startAnimationDuration).SetEase(startEase).From();

            yield return new WaitForSeconds(1f);

            onStart?.Invoke();
        }

        #endregion

        #region Movement

        public void WalkThroughPoints(Action onAttack = null)
        {
            int random = RandomNotTheSame();

            transform.position = new Vector3(transform.position.x, _startPositionY, transform.position.z);

            _currentWalkTween = transform.DOScaleY(6f, 0.6f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

            StartCoroutine(Walk(points[random], onAttack));
        }

        public IEnumerator Walk(Transform t, Action onAttack = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {

                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);

                yield return new WaitForEndOfFrame();
            }


            onAttack?.Invoke();
        }

        #endregion

        #region Attack

        public void StartAttack(Action endAttack = null)
        {

            if (_currentWalkTween != null)
            {
                _currentWalkTween.Kill();
            }


            StartCoroutine(Attack(endAttack));
        }

        public IEnumerator Attack(Action endAttack = null)
        {
            int attacks = 0;

            while (attacks < attackAmount)
            {
                Debug.Log("attack");

                attacks++;

                transform.DOScale(4f, 0.3f).SetEase(Ease.OutBack).SetLoops(2, LoopType.Yoyo);

                yield return new WaitForSeconds(timeBetweenAttack);
            }

            transform.DOScale(10f, 0.3f).SetEase(Ease.OutBack);

            transform.position = new Vector3(transform.position.x, _startPositionY, transform.position.z);

            yield return new WaitForSeconds(0.3f);

            endAttack?.Invoke();
        }

        public int RandomNotTheSame()
        {
            while (_randomPoint == _lastRandomPoint)
            {
                _randomPoint = UnityEngine.Random.Range(0, _randomPoints.Count - 1);

                if (_randomPoint != _lastRandomPoint)
                {
                    _lastRandomPoint = _randomPoint;
                    break;
                }                   
            }

            return _randomPoint;

        }

        private void OnBossKill(HealthBase health)
        {
            stateMachine.SwitchState(BossStates.DEATH);
        }

        #endregion
    }
}

