using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPool : Singleton<SfxPool>
{
    private int index;

    public int poolSize;

    private List<AudioSource> audioSourcePool;


    void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        audioSourcePool = new List<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            CreatePoolItem();
        }
    }

    private void CreatePoolItem()
    {
        GameObject go = new GameObject("Sfx_PoolItem");

        go.transform.SetParent(gameObject.transform);

        audioSourcePool.Add(go.AddComponent<AudioSource>());
    }

    public void Play(SfxType sfxType)
    {
        var setup = SoundManager.Instance.GetSfxByType(sfxType);

        if (setup == null || setup.sfxType == SfxType.NONE) return;

        audioSourcePool[index].clip = setup.clip;
        audioSourcePool[index].pitch = setup.Pitch;

        audioSourcePool[index].Play();

        index++;

        if (index >= poolSize)
            index = 0;
        
    }
       
}
