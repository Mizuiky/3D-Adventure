using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : Singleton<ScreenShake>
{
    [Header("ShakeSetup")]

    public float amplitude;
    public float frequency;
    public float time;

    private float shakeTime;

    public CinemachineVirtualCamera camera;

    private CinemachineBasicMultiChannelPerlin multiChannelPerlin;

    public void Start()
    {
        multiChannelPerlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shakeTime = 0;
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        multiChannelPerlin.m_AmplitudeGain = amplitude;
        multiChannelPerlin.m_FrequencyGain = frequency;

        shakeTime = time;
    }

    public void Shake()
    {
        Debug.Log("shake");
        Shake(amplitude, frequency, time);
    }

    public void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        { 
            if(multiChannelPerlin != null)
            {
                multiChannelPerlin.m_AmplitudeGain = 0;
                multiChannelPerlin.m_FrequencyGain = 0;
            }     
        }
    }

}
