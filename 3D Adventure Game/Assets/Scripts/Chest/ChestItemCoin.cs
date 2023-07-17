using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemCoin : ChestItemBase
{
    private List<GameObject> _items = new List<GameObject>();

    public GameObject coin;

    public int qtd;

    public float force;
    public float gravity;

    private Vector3 direction;

    public override void ShowItem()
    {
        base.ShowItem();
        CreateCoins();
    }

    public void CreateCoins()
    {
        for(int i = 0; i < qtd; i++)
        {
            var item = Instantiate(coin);
            item.transform.position = transform.position;
            item.transform.localScale = Vector3.one;

            Rigidbody body = item.GetComponentInChildren<Rigidbody>();

            if(body != null)
            {
                float randomAngle = Random.Range(0, 360);

                AddForceRelatedToAngle(body, randomAngle);

                StartCoroutine(ApplyGravity(body));
            }

            _items.Add(item);
        }
    }

    private void AddForceRelatedToAngle(Rigidbody body, float randomAngle)
    {
        float RadAngle = randomAngle * Mathf.Deg2Rad;

        float x = Mathf.Sin(RadAngle);
        float y = Mathf.Cos(RadAngle);

        direction = new Vector3(x, y, 0);

        direction.Normalize();

        body.AddForce(direction * force, ForceMode.Impulse);
    }

    private IEnumerator ApplyGravity(Rigidbody body)
    {
        yield return new WaitForSeconds(.1f);

        Debug.Log("apply force");

        body.AddForce(Vector3.down * gravity, ForceMode.Force);
    }
}
