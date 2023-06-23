using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIUpdater : MonoBehaviour
{
    public Image image;


    [Header("Animation")]
    public Ease ease = Ease.OutBack;
    public float duration = 0.1f;
    private Tween _currentTween;

    public void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
    public void UpdateValue(float max, float current)
    {
        if (_currentTween != null)
            _currentTween.Kill();

        _currentTween = image.DOFillAmount(1 - current / max, duration).SetEase(ease);
    }

    public void UpdateValue(float value)
    {
        image.fillAmount = value;
    }
}
