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

        public override void Awake()
        {
            base.Awake();
            SaveManager.Instance.fileLoaded += LoadItemsFromSave;
        }

        public void Start()
        {
           
        }

        private void OnDestroy()
        {
            SaveManager.Instance.fileLoaded -= LoadItemsFromSave;
        }

        public void Reset()
        {
            itemSetup.ForEach(x => x.so.value = 0);    
        }

        private void LoadItemsFromSave(SaveSetup setup)
        {
            /**** its not optimized, need to check out later  ****/
            loadCoins(setup);
            loadLifePack(setup);
        }

        public void loadCoins(SaveSetup setup)
        {
            ItemSetup item = itemSetup.Find(x => x.itemType == ItemType.Coin);

            if (item != null)
            {
                item.so.value = setup.coins;
            }

            OnChangeUI?.Invoke(item);
        }

        public void loadLifePack(SaveSetup setup)
        {
            ItemSetup item = itemSetup.Find(x => x.itemType == ItemType.Life_Pack);

            if (item != null)
            {
                item.so.value = (int)setup.lifePack;
            }

            OnChangeUI?.Invoke(item);
        }

        public void AddByType(ItemType type, int amount = 1)
        {
            ItemSetup item = itemSetup.Find(x => x.itemType == type);

            if(item != null)
            {
                item.so.value += amount;
            }

            OnChangeUI?.Invoke(item);

            SaveManager.Instance.SaveItems();
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

                SaveManager.Instance.SaveItems();
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

