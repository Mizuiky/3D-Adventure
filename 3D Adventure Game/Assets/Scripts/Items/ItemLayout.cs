using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        public Image icon;
        public TextMeshProUGUI amount;

        private ItemSetup _currentSetup;

        public void Awake()
        {
            Debug.Log("Item Layout");
            ItemManager.Instance.OnChangeUI += UpdateUI;
        }

        public void Load(ItemSetup setup)
        {    
            _currentSetup = setup;
            icon.sprite = _currentSetup.itemIcon;
        }

        public void UpdateUI(ItemSetup setup)
        {
            if (setup.itemType == _currentSetup.itemType)
            {
                amount.text = _currentSetup.so.value.ToString();
            }
        }
    }
}
