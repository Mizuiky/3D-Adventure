using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetup;
    public List<SfxSetup> SfxSetup;

    public AudioSource audioSource;

    public MusicSetup GetMusicByType(MusicType type)
    {
        MusicSetup setup = musicSetup.Find(x => x.musicType == type);

        if (setup != null)
            return setup;

        return null;
    }

    public SfxSetup GetSfxByType(SfxType type)
    {
        return SfxSetup.Find(x => x.sfxType == type);
    }

    public void PlayMusicByType(MusicType type)
    {
        var music = GetMusicByType(type);

        audioSource.clip = music.clip;
        audioSource.pitch = music.Pitch;
        audioSource.Play();
    }

    public void PlaySfxByType(SfxType type)
    {
        var vfx = GetSfxByType(type);

        audioSource.clip = vfx.clip;
        audioSource.pitch = vfx.Pitch;
        audioSource.Play();
    }
}

public enum MusicType
{
    NONE,
    Type1,
    Type2,
    Type3
}

public enum SfxType
{
    NONE,
    COIN,
    LIFE,
    CHEST
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip clip;
    public float Pitch;
}

[System.Serializable]
public class SfxSetup
{
    public SfxType sfxType;
    public AudioClip clip;
    public float Pitch;
}