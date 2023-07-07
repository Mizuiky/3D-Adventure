using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChecktTrigger : MonoBehaviour
{
    public Color gizmosColor;
    public GameObject bossCamera;
    public SphereCollider collider;

    public string tagToCollide;


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagToCollide))
        {
            EnableBossCamera();
        }
    }

    private void EnableBossCamera()
    {
        bossCamera.SetActive(true);       
    }

    private void DisableBossCamera()
    {
        bossCamera.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(transform.position, collider.radius);
    }
}
