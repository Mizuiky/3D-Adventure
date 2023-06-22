using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer mesh;
    public SkinnedMeshRenderer skinnedMesh;

    [Header("Setup")]
    public Color color = Color.red;
    public float animationDuration;

    private Color initialColor;
    private Tween _currentTween;

    private void OnValidate()
    {
        if(skinnedMesh == null)
            skinnedMesh = GetComponent<SkinnedMeshRenderer>();
        if (mesh == null)
            mesh = GetComponent<MeshRenderer>();

    }

    public void Start()
    {
        if (mesh != null)
            initialColor = mesh.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void ChangeColor()
    {
        Debug.Log("change color");
        if (mesh != null && !_currentTween.IsActive())
            _currentTween = mesh.material.DOColor(color, "_EmissionColor", animationDuration).SetLoops(2, LoopType.Yoyo);

        if (skinnedMesh != null && !_currentTween.IsActive())
            _currentTween = skinnedMesh.material.DOColor(color, "_EmissionColor", animationDuration).SetLoops(2, LoopType.Yoyo);
    }
}
