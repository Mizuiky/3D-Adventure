using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CheckpointBase : MonoBehaviour
{
    public int key;
    private string checkPointKey = "CheckpointKey";
    public MeshRenderer mesh;

    private bool isActivated = false;

    private void Start()
    {
        PlayerPrefs.DeleteKey(checkPointKey);

        TurnOff();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isActivated)
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        SaveCheckPoint();

        TurnOn();
        isActivated = true;
    }

    private void SaveCheckPoint()
    {
        Debug.Log("player prefs" + PlayerPrefs.GetInt(checkPointKey, 0));
        Debug.Log("current checkpoint key" + key);

        if (PlayerPrefs.GetInt(checkPointKey, 0) < key)
        {
            Debug.Log("save");
            PlayerPrefs.SetInt(checkPointKey, key);
        }
        
    }

    private void TurnOn()
    {
        mesh.material.SetColor("_EmissionColor", Color.white);
    }

    private void TurnOff()
    {
        mesh.material.SetColor("_EmissionColor", Color.black);
    }
}
