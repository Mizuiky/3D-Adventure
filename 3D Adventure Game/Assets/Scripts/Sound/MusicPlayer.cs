using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;
    public AudioSource audioSource;

    public MusicSetup _currentMusicSetup;

    public void Start()
    {
        Play();
    }

    public void Play()
    {
        var setup = SoundManager.Instance.GetMusicByType(musicType);

        _currentMusicSetup = setup;

        audioSource.clip = setup.clip;
        audioSource.pitch = setup.Pitch;
        audioSource.Play();
    }
}
