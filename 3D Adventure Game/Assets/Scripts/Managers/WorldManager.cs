using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMove player;
    public static WorldManager Instance;

    public PlayerMove Player { get { return player;  } }

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
    }
}