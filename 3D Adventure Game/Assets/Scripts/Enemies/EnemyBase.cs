using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public int _currentLife;

        public int _startLife;

        [SerializeField]
        private EnemyAnimationBase enemyAnimation;

        private void Awake()
        {
            ResetLife();
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

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5);
            }
        }
    }
}
