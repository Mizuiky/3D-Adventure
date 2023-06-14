using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gun;

    protected override void Init()
    {
        base.Init();

        inputs.GamePlay.Shoot.performed += callBack => OnStartShoot();
        inputs.GamePlay.Shoot.canceled += callBack => OnEndShoot();
    }

    private void OnStartShoot()
    {
        gun.StartShoot();
    }

    private void OnEndShoot()
    {
        gun.EndShoot();
    }

}
