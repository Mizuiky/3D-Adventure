using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.IO;

public class SaveManager : MonoBehaviour
{
    
    [NaughtyAttributes.Button]
    public void Save()
    {
        SaveSetup save = new SaveSetup();

        save.lastLevel = 1;
        save.playerName = "Priscila";

        string jsonSetup = JsonUtility.ToJson(save, true);
        Debug.Log(jsonSetup);

        SaveFile(jsonSetup);
    }

    public void SaveFile(string jsonFile)
    {
        string path = Application.dataPath + "/save.txt";
        string fileLoaded = "";

        if (File.Exists(path))
        {
            Debug.Log("File Exist");
            fileLoaded = File.ReadAllText(path);
            Debug.Log(fileLoaded);
        }
        else
        {
            File.WriteAllText(path, jsonFile);
        }
    }

        

      
}

public class SaveSetup
{
    public int lastLevel;
    public string playerName;
}
