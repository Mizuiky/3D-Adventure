using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public string tagToCompare = "Player";
        public ParticleSystem particle;
        public float timeToHide = 2f;

        public GameObject graphicObject;

        public Collider collider;

        public ItemType itemType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagToCompare))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            if(collider != null)
                collider.enabled = false;

            if (graphicObject != null)
                graphicObject.SetActive(false);

            Invoke("HideOnCollect", timeToHide);

            OnCollect();

        }

        private void HideOnCollect()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnCollect()
        {
            if (particle != null)
                particle.Play();
        }
    }
}

