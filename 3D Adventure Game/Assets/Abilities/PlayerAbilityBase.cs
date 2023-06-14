using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour
{
    protected Player player;
    protected Inputs inputs;

    protected void OnValidate()
    {
        if (player == null)
            player = GetComponent<Player>();
    }
    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();

        Init();
        RegisterListeners();
    }

    private void OnEnable()
    {
        if (inputs != null)
            inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    protected virtual void Init() { }
    protected virtual void RegisterListeners() { }
    protected virtual void RemoveListeners() { }

    public void OnDestroy()
    {
        RemoveListeners();
    }
}
