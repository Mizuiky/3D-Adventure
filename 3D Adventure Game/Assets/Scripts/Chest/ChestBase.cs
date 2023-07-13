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

    public void Start()
    {
        HideGraphic();
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

    public virtual void OpenChest()
    {
        if(chestAnimator != null)
        {
            chestAnimator.SetTrigger(chestTrigger);
        }
    }

    public void HideGraphic()
    {
        graphic.SetActive(false);
    }

    public void ShowGraphic()
    {
        graphic.SetActive(true);
    }

    
}
