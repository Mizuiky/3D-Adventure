using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableLifePack : ItemCollectableBase
    {
        public override void OnCollect()
        {
            base.OnCollect();
            ItemManager.Instance.AddByType(itemType, 1);
        }
    }
}

