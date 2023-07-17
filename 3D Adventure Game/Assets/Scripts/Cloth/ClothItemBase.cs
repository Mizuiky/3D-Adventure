using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType type;
        public float duration;

        public string tagToCompare = "Player";

        public void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(tagToCompare))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            HideItem();

            ClothSetup setup = WorldManager.Instance.ClothManager.GetClothByType(type);
            WorldManager.Instance.Player.ChangeCloth(setup, duration);
        }

        private void HideItem()
        {
            gameObject.SetActive(false);
        }
    }
}

