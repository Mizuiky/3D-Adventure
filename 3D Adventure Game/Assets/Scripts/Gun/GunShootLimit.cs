using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    private int _currentShoot;
    public float _timeToRecharge = 1f;
    public int _shootLimit = 5;
    public bool _isRecharging = false;

    protected override IEnumerator StartToShoot()
    {
        if (_isRecharging) yield break;

        while (true)
        {
            if(_currentShoot < _shootLimit)
            {
                Shoot();
                _currentShoot++;
                UpdateUi();
                CheckRecharge();
                yield return new WaitForSeconds(_timeBetweenShoots);
            }       
        }
    }

    private void CheckRecharge()
    {
        if(_currentShoot >= _shootLimit)
        {
            EndShoot();
            StartCoroutine(RechargeShoot());
        }      
    }

    private IEnumerator RechargeShoot()
    {
        _isRecharging = true;

        float time = 0;

        while(time < _timeToRecharge)
        {
            time += Time.deltaTime;
            uIGunUpdater.ForEach(i => i.UpdateValue(time / _timeToRecharge));
            yield return new WaitForEndOfFrame();
        }

        _currentShoot = 0;
        _isRecharging = false;
    }

    private void UpdateUi()
    {
        Debug.Log("updateUi");
        uIGunUpdater.ForEach(i => i.UpdateValue(_shootLimit, _currentShoot));
    }
}
