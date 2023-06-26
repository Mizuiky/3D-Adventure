using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using NaughtyAttributes;


public class EffectManager : Singleton<EffectManager>
{
    public float duration = 0.3f;
    public PostProcessVolume processVolume;

    [SerializeField]
    private Vignette _vignette;

    [NaughtyAttributes.Button]
    public void ChangeVinhetColor()
    {
        StartCoroutine(DamageCameraEffect());
    }

    [NaughtyAttributes.Button]
    public IEnumerator DamageCameraEffect()
    {
        float time = 0;

        Vignette v;

        if (processVolume.profile.TryGetSettings<Vignette>(out v))
        {
            _vignette = v;
        }

        ColorParameter color = new ColorParameter();

        while (time < duration)
        {
            color.value = Color.Lerp(Color.red, Color.white, time/duration);
            _vignette.color.Override(color);

            _vignette.intensity.Override(.5f);
                
            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        _vignette.intensity.Override(0);
    }
}
