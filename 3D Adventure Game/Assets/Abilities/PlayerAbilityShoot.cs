using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<GunBase> _gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;

    public List<UIUpdater> uIGunUpdater;

    protected override void Init()
    {
        base.Init();

        CreateGun(0);

        inputs.GamePlay.Shoot.performed += callBack => OnStartShoot();
        inputs.GamePlay.Shoot.canceled += callBack => OnEndShoot();

        inputs.GamePlay.Gun1.performed += callback => CreateGun1();
        inputs.GamePlay.Gun2.performed += callback => CreateGun2();
    }

    private void CreateGun(int index)
    {
        _currentGun = Instantiate(_gunBase[index], gunPosition);
        _currentGun.transform.position = _currentGun.transform.eulerAngles = gunPosition.position;
        _currentGun.transform.rotation = gunPosition.transform.rotation;
        _currentGun.uIGunUpdater = uIGunUpdater;

        _currentGun.gameObject.SetActive(true);
    }

    private void CreateGun1()
    {
        if (_currentGun != null)
        {
            Destroy(_currentGun.gameObject);
        }

        CreateGun(0);
    }

    private void CreateGun2()
    {
        if (_currentGun != null)
        {
            Destroy(_currentGun.gameObject);
        }

        CreateGun(1);
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
