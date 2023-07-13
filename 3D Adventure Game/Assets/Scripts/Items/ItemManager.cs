using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public enum ItemType
    {
        Coin,
        Life_Pack
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetup = new List<ItemSetup>();

        public event Action<ItemSetup> OnChangeUI;

        public void Start()
        {
            Reset();
        }

        public void Reset()
        {
            itemSetup.ForEach(x => x.so.value = 0);    
        }

        public void AddByType(ItemType type, int amount = 1)
        {
            ItemSetup item = itemSetup.Find(x => x.itemType == type);

            if(item != null)
            {
                item.so.value += amount;
            }

            OnChangeUI?.Invoke(item);
        }

        public ItemSetup GetByType(ItemType type)
        {
            ItemSetup item = itemSetup.Find(x => x.itemType == type);

            if (item != null)
            {
                return item;
            }

            return null;
        }

        public void RemoveByType(ItemType type, int amount = 1)
        {
            ItemSetup item = itemSetup.Find(x => x.itemType == type);

            if (item != null)
            {
                item.so.value -= amount;

                OnChangeUI?.Invoke(item);
            }
        }
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt so;
        public Sprite itemIcon;
    }
}

