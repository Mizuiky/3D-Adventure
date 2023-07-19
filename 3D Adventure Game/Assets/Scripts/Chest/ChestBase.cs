using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    private string tagToCompare = "Player";

    public Animator chestAnimator;
    public Animator arrowAnimator;

    public string chestTrigger;
    public string arrowTrigger;

    public GameObject graphic;

    public KeyCode keyToOpen;

    private bool chestOpened;

    [Space]
    public ChestItemBase chestItem;
    public float showItemDelay = 2f;

    public ParticleSystem particle;

    public void Start()
    {
        HideGraphic();
        chestOpened = false;
    }

    public void Update()
    {
        if(Input.GetKeyDown(keyToOpen) && !chestOpened)
        {
            OpenChest();
        }  
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagToCompare))
        {
            ShowGraphic();
            arrowAnimator.SetBool(arrowTrigger, true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        arrowAnimator.SetBool(arrowTrigger, false);
        HideGraphic();
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        particle?.Play();
    }

    public virtual void OpenChest()
    {
        if (chestOpened) return;

        if(chestAnimator != null)
        {
            chestAnimator.SetTrigger(chestTrigger);
        }

        Invoke("ShowItem", showItemDelay);

        chestOpened = true;
    }

    private void HideGraphic()
    {
        graphic.SetActive(false);
    }

    private void ShowGraphic()
    {
        graphic.SetActive(true);
    }

    
}
