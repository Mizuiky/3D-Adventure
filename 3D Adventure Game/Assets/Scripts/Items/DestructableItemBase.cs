using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class DestructableItemBase : MonoBehaviour
{
    public float shakeDuration;
    public int shakeForce;

    public HealthBase health;

    public GameObject itemPrefab;
    public int itemAmountToDrop;

    public Transform dropPosition;

    public void OnValidate()
    {
        if (health == null) health = GetComponent<HealthBase>();
    }

    private void Start()
    {
        health.OnDamage += Damage;
        health.OnKill += Kill;
    }

    public void Damage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up/2, shakeForce);
        DropGroupItems();
    }

    private void Kill(HealthBase h)
    {
        gameObject.SetActive(false);
    }

    [NaughtyAttributes.Button]
    private void Drop()
    {
        var item = Instantiate(itemPrefab);
        item.transform.position = dropPosition.position;
        item.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
    }


    [NaughtyAttributes.Button]
    private void DropGroupItems()
    {
        StartCoroutine(DropItems());
    }

    private IEnumerator DropItems()
    {
        for(int i = 0; i < itemAmountToDrop; i++)
        {
            Drop();
            yield return new WaitForSeconds(.1f);
        }
    }
}
