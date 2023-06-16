using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [Header("Gun Settings")]

    public ProjectileBase _projectil;

    public Transform _positionToShoot;

    private Coroutine _currentCoroutine;

    public float _timeBetweenShoots;

    public float speed = 50f;

    public List<UIGunUpdater> uIGunUpdater;

    public void StartShoot()
    {
        EndShoot();
        _currentCoroutine = StartCoroutine(StartToShoot());
    }

    public void EndShoot()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }

    protected virtual IEnumerator StartToShoot()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(_timeBetweenShoots);
        } 
    }

    protected virtual void Shoot()
    {
        //create pool of shoot its better
        var shoot = Instantiate(_projectil, _positionToShoot);
        shoot.transform.position = _positionToShoot.position;
        shoot.speed = speed;

        shoot.transform.parent = null;
    }
}
