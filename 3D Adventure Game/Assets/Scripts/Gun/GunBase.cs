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
    [SerializeField]
    private float _timeBetweenShoots;
    [SerializeField]
    private KeyCode _shootKey;

    private Coroutine _currentCoroutine;

    public void Update()
    {
        if(Input.GetKeyDown(_shootKey))
            _currentCoroutine = StartCoroutine(StartToShoot());

        else if(Input.GetKeyUp(_shootKey))
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }

    private IEnumerator StartToShoot()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(_timeBetweenShoots);
        } 
    }

    private void Shoot()
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
