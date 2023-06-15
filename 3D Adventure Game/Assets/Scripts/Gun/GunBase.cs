using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [Header("Gun Settings")]

    [SerializeField]
    private ProjectileBase _projectil;
    [SerializeField]
    private Transform _positionToShoot;

    private Coroutine _currentCoroutine;

    public float _timeBetweenShoots;

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

    protected void Shoot()
    {
        //create pool of shoot its better
        var shoot = Instantiate(_projectil, _positionToShoot);
        shoot.transform.parent = null;

        if(shoot != null)
        {
            var projectil = shoot.GetComponent<ProjectileBase>();
            if (projectil != null)
                projectil.Init(_positionToShoot);
        }
    }
}
