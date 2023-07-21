using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckpoint;

    public List<CheckpointBase> checkpoints;

    private string checkPointKey = "CheckpointKey";

    [SerializeField]
    private GameObject checkpointContainer;

    public override void Awake()
    {
        base.Awake();
        SaveManager.Instance.fileLoaded += SetCurrentLastCheckpoint;
    }

    private void OnDestroy()
    {
        SaveManager.Instance.fileLoaded -= SetCurrentLastCheckpoint;
    }

    private void SetCurrentLastCheckpoint(SaveSetup setup)
    {
        lastCheckpoint = setup.lastCheckpoint;
    }

    public void SaveCheckpoint(int index)
    {
        if(index > lastCheckpoint)
        {
            PlayerPrefs.SetInt(checkPointKey, index);
            lastCheckpoint = index;
            SaveManager.Instance.SaveLastCheckpoint(lastCheckpoint);
        }           
    }

    public Vector3 GetLastCheckPointPosition()
    {
        CheckpointBase checkpoint = checkpoints.Find(i => i.key == lastCheckpoint);

        if(checkpoint != null)
        {
            Debug.Log("get last checkpoint" + checkpoint.key);

            return checkpoint.respawPoint.position;
        }
           
        return Vector3.zero;
    }
}
