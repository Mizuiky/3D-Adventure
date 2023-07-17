using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType type;

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
        }

        private void HideItem()
        {
            gameObject.SetActive(false);
        }


    }
}

