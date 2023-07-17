using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;
        public Texture texture;

        public string shaderIdName = "_EmissionMap";


        [NaughtyAttributes.Button]
        public void ChangeCloth()
        {
            mesh.materials[0].SetTexture(shaderIdName, texture);
        }
    }
}

