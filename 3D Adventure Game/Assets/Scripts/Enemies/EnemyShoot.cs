using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gun;

        protected override void Init()
        {
            base.Init();

            gun.StartShoot();
        }
    }
}

