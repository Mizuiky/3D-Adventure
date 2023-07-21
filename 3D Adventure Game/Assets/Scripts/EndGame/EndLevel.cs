using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public ParticleSystem endLevelParticle;
    public List<GameObject> objectsToShow;
    public int levelIndex;

    public string tagToCompare = "Player";

    public void Start()
    {
        foreach (GameObject objects in objectsToShow)
        {
            objects.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagToCompare))
        {
            OnEndLevel();
        }
    }

    public void OnEndLevel()
    {
        foreach(GameObject objects in objectsToShow)
        {
            objects.SetActive(true);
        }

        endLevelParticle.Play();

        SaveManager.Instance.SaveLevel(levelIndex);
    }
}
