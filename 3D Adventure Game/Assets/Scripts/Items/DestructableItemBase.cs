using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public float shakeDuration;
    public int shakeForce;

    public HealthBase health;

    public void OnValidate()
    {
        if (health == null) health = GetComponent<HealthBase>();
    }

    private void Start()
    {
        health.OnDamage += Damage;
    }

    public void Damage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, shakeForce);
    }
}
