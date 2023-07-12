using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableCoin : ItemCollectableBase
    {
        public override void OnCollect()
        {
            base.OnCollect();
            ItemManager.Instance.AddByType(itemType, 2);
        }
    }
}

