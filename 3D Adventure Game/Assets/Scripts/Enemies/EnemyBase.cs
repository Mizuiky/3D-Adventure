using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using animation;
using DG.Tweening;

namespace enemy
{
    public class EnemyBase : MonoBehaviour, IDamageble
    {
        public int _currentLife;

        public int _startLife;

        public int enemyDamage = 10;

        public FlashColor flashColor;

        [SerializeField]
        private EnemyAnimationBase enemyAnimation;

        [SerializeField]
        private ParticleSystem particle;


        public bool lookAtPlayer;
        private PlayerMove _player;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private void Awake()
        {
            ResetLife();
            bornAnimation();
        }

        public virtual void Start()
        {
            Init();
        }

        public virtual void Update()
        {
            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }

        protected virtual void Init()
        {
            _player = WorldManager.Instance.Player;
        }

        protected virtual void ResetLife()
        {
            _currentLife = _startLife;
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            Destroy(gameObject, 3f);
            PlayAnimationByType(animationType.DEATH);            
        }

        private void PlayAnimationByType(animationType type)
        {
            enemyAnimation.PlayAnimationByType(type);
        }

        protected virtual void OnDamage(int value)
        {
            if(flashColor != null)
                flashColor.ChangeColor();

            if(particle != null)
                particle.Emit(15);

            _currentLife -= value;

            if (_currentLife <= 0)
                Kill();
        }

        public void Damage(int value)
        {
            Debug.Log("damage");
            OnDamage(value);
        }

        public void Damage(int value, Vector3 dir)
        {
            Debug.Log("damage");

            OnDamage(value);
            transform.DOMove(transform.position - dir, .1f);       
        }

        public void OnCollisionEnter(Collision collision)
        {
            PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();

            if(player != null)
            {
                player.Damage(enemyDamage);
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
