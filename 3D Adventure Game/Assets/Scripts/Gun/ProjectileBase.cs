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
    private int _damageAmount;

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
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colision");

        var dmg = collision.gameObject.GetComponent<IDamageble>();
        
        if (dmg != null)
        {
            Debug.Log("dmg != null");

            var direction = collision.gameObject.transform.position - transform.position;
            direction = -direction.normalized;
            direction.y = 0;

            dmg.Damage(_damageAmount, direction);
            Destroy(gameObject);
        }
         
    }
}
