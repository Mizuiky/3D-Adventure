using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.IO;
using Items;

public class SaveManager : MonoBehaviour
{
    private SaveSetup _saveSetup;

    public void Awake()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 1;
        _saveSetup.playerName = "Priscila";
        DontDestroyOnLoad(gameObject);
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

    public void SaveLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();     
    } 

    public void SaveItems()
    {
        _saveSetup.coins = ItemManager.Instance.GetByType(ItemType.Coin).so.value;
        _saveSetup.health = ItemManager.Instance.GetByType(ItemType.Life_Pack).so.value;
    }
}

public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public int coins;
    public float health;
}
