using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : Singleton<ScreenShake>
{
    [Header("ShakeSetup")]

    private float _shakeTime;

    public CinemachineVirtualCamera [] virtualCam;

    private CinemachineBasicMultiChannelPerlin [] _multiChannelPerlin;

    private Coroutine _currentCoroutine;

    private int _currentChannel;

    public void Start()
    {
        _multiChannelPerlin = new CinemachineBasicMultiChannelPerlin[virtualCam.Length];

        for (int i = 0; i < virtualCam.Length; i++)
        {
            _multiChannelPerlin[i] = virtualCam[i].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    public void Shake(float amplitude, float frequency, float time, int channel)
    { 
        Debug.Log("shake 1");

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);

            _multiChannelPerlin[_currentChannel].m_AmplitudeGain = 0;
            _multiChannelPerlin[_currentChannel].m_FrequencyGain = 0;
        }
            
        _currentCoroutine = StartCoroutine(ShakeCamera(amplitude, frequency, time, channel));
    }

    public IEnumerator ShakeCamera(float amplitude, float frequency, float time, int channel)
    {
        Debug.Log("shake 2");

        _currentChannel = channel;

        _multiChannelPerlin[channel].m_AmplitudeGain = amplitude;
        _multiChannelPerlin[channel].m_FrequencyGain = frequency;

        Debug.Log("shake 3");
        yield return new WaitForSeconds(time);

        Debug.Log("shake 4");

        _multiChannelPerlin[channel].m_AmplitudeGain = 0;
        _multiChannelPerlin[channel].m_FrequencyGain = 0;

    }

}
