using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cloth
{
    public class ClothItemStrong : ClothItemBase
    {
        public float damageMultiplier;

        public override void Collect()
        {
            base.Collect();

            WorldManager.Instance.Player.healthBase.ReduceDamage(damageMultiplier, duration);
        }
    }
}

