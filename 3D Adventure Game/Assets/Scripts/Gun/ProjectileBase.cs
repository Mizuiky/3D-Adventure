using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [Header("Projectile Settings")]

    [SerializeField]
    public float speed = 50f;
    [SerializeField]
    private float _timeToDestroy = 2f;
    [SerializeField]
    private float _damageAmount;

    //particle to show

    private void Awake()
    {
        Invoke("OnDestroy", _timeToDestroy);
    }

    public void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
