using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private SaveSetup _saveSetup;

    public void Awake()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 1;
        _saveSetup.playerName = "Priscila";
    }

    [NaughtyAttributes.Button]
    public void Save()
    {
        string jsonSetup = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(jsonSetup);

        SaveFile(jsonSetup);
    }

    public void SaveFile(string jsonFile)
    {
        string path = Application.dataPath + "/save.txt";
        string fileLoaded = "";

        //if (File.Exists(path))
        //{
        //    Debug.Log("File Exist");
        //    fileLoaded = File.ReadAllText(path);
        //    Debug.Log(fileLoaded);
        //    SaveSetup loadedSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
        //}
        //else
        //{
            File.WriteAllText(path, jsonFile);
        //}
    }

    [NaughtyAttributes.Button]
    public void SaveLevelTwo()
    {
        _saveSetup.lastLevel = 2;
        Save();
    }

        

      
}

public class SaveSetup
{
    public int lastLevel;
    public string playerName;
}
