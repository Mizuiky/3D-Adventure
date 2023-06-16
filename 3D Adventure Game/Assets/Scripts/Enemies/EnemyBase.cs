using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using animation;
using DG.Tweening;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageble
    {
        public int _currentLife;

        public int _startLife;

        [SerializeField]
        private EnemyAnimationBase enemyAnimation;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private void Awake()
        {
            ResetLife();
            bornAnimation();
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
            _currentLife -= value;

            if (_currentLife <= 0)
                Kill();
        }

        public void Damage(int value)
        {
            Debug.Log("damage");
            OnDamage(value);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5);
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
