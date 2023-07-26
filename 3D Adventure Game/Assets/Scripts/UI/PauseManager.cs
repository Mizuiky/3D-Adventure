using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseManager : MonoBehaviour
{
    public GameObject pauseContainer;
    private void Start()
    {
        gameObject.SetActive(true);
        pauseContainer.SetActive(false);
    }

    public void Pause()
    {
        pauseContainer.SetActive(true);
        Time.timeScale = 0;       
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseContainer.SetActive(false);
    }
}
