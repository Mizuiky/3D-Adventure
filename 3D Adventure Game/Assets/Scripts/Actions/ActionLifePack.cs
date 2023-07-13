using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ActionLifePack : MonoBehaviour
{
    private SOInt soInt;

    public void Start()
    {
        soInt = ItemManager.Instance.GetByType(ItemType.Life_Pack).so;
    }

    public void LifeRecover()
    {
        if(soInt.value > 0)
        {
            WorldManager.Instance.Player.healthBase.ResetLife();
            ItemManager.Instance.RemoveByType(ItemType.Life_Pack);
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            LifeRecover();
        }
    }

}
