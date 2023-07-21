using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cloth;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMove player;

    [SerializeField]
    private ClothManager clothManager;

    public static WorldManager Instance;

    public bool finishGamePlay = false;
    public PlayerMove Player { get { return player;  } }
    public ClothManager ClothManager { get { return clothManager; } }

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = GetComponent<WorldManager>();
        }
        else
        {
            Destroy(Instance.gameObject);
        }

        finishGamePlay = false;

        player.OnEndGame += FinishGamePlay;
    }

    public void FinishGamePlay(bool endGame)
    {
        if(endGame)
        {
            finishGamePlay = true;
        }       
    }
}
