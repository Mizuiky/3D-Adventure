using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using animation;
using DG.Tweening;
using UnityEngine.Events;

namespace enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public int enemyDamage = 10;

        public FlashColor flashColor;

        [SerializeField]
        private EnemyAnimationBase enemyAnimation;

        [SerializeField]
        private ParticleSystem particle;

        public HealthBase healthBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        protected bool isAlive = true;

        public UnityEvent OnEnemyKilled;

        private void Awake()
        {
            bornAnimation();
        }

        public virtual void Start()
        {
            Init();
        }

        public virtual void Update()
        {

        }

        protected virtual void Init()
        {
            healthBase.OnDamage += OnDamage;
            healthBase.OnKill += OnKill;
            WorldManager.Instance.Player.OnEndGame += OnEndGame;
        }

        protected virtual void OnKill(HealthBase h)
        {
            Destroy(gameObject, 3f);
            PlayAnimationByType(animationType.DEATH);
            OnEnemyKilled?.Invoke();
        }

        private void PlayAnimationByType(animationType type)
        {
            enemyAnimation.PlayAnimationByType(type);
        }

        protected virtual void OnDamage(HealthBase h)
        {
            if (flashColor != null)
                flashColor.ChangeColor();

            if (particle != null)
                particle.Emit(15);

            transform.DOMove(transform.position - h._currentHitDirection, .1f);
        }

        public void OnCollisionEnter(Collision collision)
        {
            PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();

            if (player != null && player._isAlive)
            {
                player.healthBase.Damage(enemyDamage);
            }
        }

        public virtual void OnEndGame(bool endGame)
        {
            if(endGame)
            {
                Debug.Log("end game enemy");
                isAlive = false;
                StopAllCoroutines();
            }                   
        }
        
        #region Animations

        private void bornAnimation()
        {
            //using from It starts the animation from 0 to the original scale
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        #endregion
    }
}
