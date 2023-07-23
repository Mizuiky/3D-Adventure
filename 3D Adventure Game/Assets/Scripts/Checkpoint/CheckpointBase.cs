using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CheckpointBase : MonoBehaviour
{
    public int key;

    public MeshRenderer mesh;

    private bool isActivated = false;

    public Transform respawPoint;

    public Transform checkpointParent;

    private Vector3 checkPointPosition;

    private void Start()
    {
        TurnOff();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isActivated)
            CheckCheckpoint();
    }

    private void CheckCheckpoint()
    {
        CheckPointManager.Instance.SaveCheckpoint(key);

        TurnOn();
        isActivated = true;
    }

    private void TurnOn()
    {
        mesh.material.SetColor("_EmissionColor", Color.white);
    }

    private void TurnOff()
    {
        mesh.material.SetColor("_EmissionColor", Color.black);
    }

    public Vector3 GetCheckpointPosition()
    {
        Vector3 localPosition = respawPoint.localPosition;
        Vector3 globalPosition = checkpointParent.TransformPoint(localPosition);

        checkPointPosition = globalPosition;

        return globalPosition;
    }
}
