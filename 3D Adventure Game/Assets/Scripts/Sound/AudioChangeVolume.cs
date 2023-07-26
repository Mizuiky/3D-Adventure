using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string audioParameter;

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat(audioParameter, volume);
    }
}
