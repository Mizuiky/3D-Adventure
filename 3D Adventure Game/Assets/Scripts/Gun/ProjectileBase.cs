using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [Header("Projectile Settings")]

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _timeToDestroy;
    [SerializeField]
    private float _damageAmount;
    
    //particle to show

    public void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void Init(Transform shootPosition)
    {
        transform.position = shootPosition.position;
        transform.rotation = shootPosition.rotation;

        Invoke("OnDestroy", _timeToDestroy);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
