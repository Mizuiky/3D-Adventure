using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public float _currentShoot;
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
                Debug.Log("current shoot" + _currentShoot);
                CheckRecharge();
                yield return new WaitForSeconds(_timeBetweenShoots);
            }       
        }
    }

    private void CheckRecharge()
    {
        if(_currentShoot >= _shootLimit)
        {
            Debug.Log("current shoot" + _currentShoot);
            Debug.Log("end shoot");
            EndShoot();
            StartCoroutine(RechargeShoot());
        }      
    }

    private IEnumerator RechargeShoot()
    {
        Debug.Log("start recharge");

        _isRecharging = true;

        float time = 0;

        while(time < _timeToRecharge)
        {
            time += Time.deltaTime;
            Debug.Log("time:" + time);
            yield return new WaitForEndOfFrame();
        }

        _currentShoot = 0;
        _isRecharging = false;
    }
}
