using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer mesh;

    [Header("Setup")]
    public Color color = Color.red;
    public float animationDuration;

    private Color initialColor;
    private Tween _currentTween;

    public void Start()
    {
        initialColor = mesh.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void ChangeColor()
    {
        if (!_currentTween.IsActive())
            _currentTween = mesh.material.DOColor(color, "_EmissionColor", animationDuration).SetLoops(2, LoopType.Yoyo);
    }
}
