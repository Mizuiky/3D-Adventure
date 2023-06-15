using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    private float _currentShoot;
    public float _timeToRecharge = 1f;
    public float _shootLimit = 5;
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

            yield return new WaitForEndOfFrame();
        }

        _currentShoot = 0;
        _isRecharging = false;
    }
}
