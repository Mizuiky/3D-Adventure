using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class MagneticTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        ItemCollectableCoin item = other.GetComponent<ItemCollectableCoin>();
        if(item != null)
        {
            item.gameObject.AddComponent<Magnetic>();
        }
    }
}
