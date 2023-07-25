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

        public SfxType sfxType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagToCompare))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            PlaySfx();

            if (collider != null)
                collider.enabled = false;

            if (graphicObject != null)
                graphicObject.SetActive(false);

            Invoke("HideOnCollect", timeToHide);

            OnCollect();

        }

        public void PlaySfx()
        {
            SfxPool.Instance.Play(sfxType);
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

