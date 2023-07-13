using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ActionLifePack : MonoBehaviour
{
    private SOInt soInt;
    private HealthBase playerHealth;

    public void Start()
    {
        soInt = ItemManager.Instance.GetByType(ItemType.Life_Pack).so;
        playerHealth = WorldManager.Instance.Player.healthBase;
    }

    public void LifeRecover()
    {
        if (soInt.value > 0 && playerHealth._currentLife < playerHealth.StartLife)
        {
            playerHealth.ResetLife();
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
