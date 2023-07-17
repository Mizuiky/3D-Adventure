using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cloth
{
    public class ClothItemSpeed : ClothItemBase
    {
        public float speed;

        public override void Collect()
        {
            base.Collect();
            WorldManager.Instance.Player.ChangeSpeed(speed, duration);
        }
    }
}

