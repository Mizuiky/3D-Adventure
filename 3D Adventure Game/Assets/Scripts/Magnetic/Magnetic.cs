using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    private float distance = 1f;
    private float magneticSpeed = 2f;

    public void Update()
    {
        if(Vector3.Distance(transform.position, WorldManager.Instance.Player.transform.position) > distance)
        {
            magneticSpeed += 1f;
            transform.position = Vector3.MoveTowards(transform.position, WorldManager.Instance.Player.transform.position, Time.deltaTime * magneticSpeed);
        }
    }
}
