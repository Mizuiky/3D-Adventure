using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.IO;
using Items;
using cloth;
using System;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField]
    private SaveSetup _saveSetup;

    private string path = Application.dataPath + "/save.txt";

    public Action<SaveSetup> fileLoaded;

    public SaveSetup CurrentSave
    {
        get { return _saveSetup;  }
    }

    public override void Awake()
    {
        base.Awake();
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
        File.WriteAllText(path, jsonFile);      
    }

    [NaughtyAttributes.Button]
    public void Load()
    {
        string file = "";

        if(File.Exists(path))
        {
            file = File.ReadAllText(path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(file);
        }
        else
        {
            CreateNewSave();
            Save();
        }
    }

    public void LoadFromFile()
    {
        fileLoaded?.Invoke(_saveSetup);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "";
        _saveSetup.coins = 0;
        _saveSetup.lifePack = 0;
        _saveSetup.CurrentPlayerHealth = 100;
        _saveSetup.StartLife = 100;
        _saveSetup.lastCheckpoint = 0;
        _saveSetup.clothType = ClothType.DEFAULT;
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
        _saveSetup.lifePack = ItemManager.Instance.GetByType(ItemType.Life_Pack).so.value;
        Save();
    }

    public void SaveLastCheckpoint(int checkpoint)
    {
        _saveSetup.lastCheckpoint = checkpoint;
        Save();
    }

    public void SaveClothType(ClothType type)
    {
        _saveSetup.clothType = type;
        Save();
    }

    public void SavePlayerHealth(int health)
    {
        _saveSetup.CurrentPlayerHealth = health;
        Save();
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public int coins;
    public float lifePack;
    public float CurrentPlayerHealth;
    public int StartLife;
    public int lastCheckpoint;
    public ClothType clothType;
}
