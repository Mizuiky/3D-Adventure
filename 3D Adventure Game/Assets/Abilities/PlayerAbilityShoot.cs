using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase _gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.GamePlay.Shoot.performed += callBack => OnStartShoot();
        inputs.GamePlay.Shoot.canceled += callBack => OnEndShoot();
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(_gunBase, gunPosition);
        _currentGun.transform.position = _currentGun.transform.eulerAngles = gunPosition.position;
        _currentGun.gameObject.SetActive(true);
    }

    private void OnStartShoot()
    {
        _currentGun.StartShoot();
    }

    private void OnEndShoot()
    {
        _currentGun.EndShoot();
    }

}
