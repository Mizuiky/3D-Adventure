using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckpoint;

    public List<CheckpointBase> checkpoints;

    private string checkPointKey = "CheckpointKey";

    public void Start()
    {
    }

    public void SaveCheckpoint(int index)
    {
        if(index > lastCheckpoint)
        {
            PlayerPrefs.SetInt(checkPointKey, index);
            lastCheckpoint = index;
        }           
    }

    public Vector3 GetLastCheckPointPosition()
    {
        CheckpointBase checkpoint = checkpoints.Find(i => i.key == lastCheckpoint);

        if(checkpoint != null)
            return checkpoint.respawPoint.position;

        return Vector3.zero;
    }
}
