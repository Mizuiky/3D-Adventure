using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemLayoutManager : MonoBehaviour
    {
        public List<ItemLayout> itemsLayout;

        public void Awake()
        {
            LoadItems();
        }

        public void LoadItems()
        {
            var itemSetup = ItemManager.Instance.itemSetup;

            for (int i = 0; i < ItemManager.Instance.itemSetup.Count; i++)
            {
                itemsLayout[i].Load(itemSetup[i]);
            }
        }
    }
}

