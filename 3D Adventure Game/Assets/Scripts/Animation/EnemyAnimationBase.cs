using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace animation
{
    public enum animationType
    {
        NONE,
        IDLE,
        RUN,
        DEATH,
        ATTACK
    }

    public class EnemyAnimationBase : MonoBehaviour
    {
        public List<AnimationSetup> animationSetup;
        public Animator animator;

        public void PlayAnimationByType(animationType type)
        {
            var setup = animationSetup.Where(x => x.animationType == type).FirstOrDefault();
            if(setup != null)
            {
                animator.SetTrigger(setup.trigger);
            }
        }
    }

   

    [System.Serializable]
    public class AnimationSetup
    {
        public animationType animationType;
        public string trigger;
    }
}

