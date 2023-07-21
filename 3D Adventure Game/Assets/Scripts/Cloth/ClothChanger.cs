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
        private Texture defaultTexture;

        public string shaderIdName = "_EmissionMap";

        public void Start()
        {
            defaultTexture = mesh.materials[0].GetTexture(shaderIdName);
        }

        [NaughtyAttributes.Button]
        public void ChangeCloth()
        {
            mesh.materials[0].SetTexture(shaderIdName, texture);
        }

        public void ChangeCloth(ClothSetup setup)
        {
            mesh.materials[0].SetTexture(shaderIdName, setup.texture);
            WorldManager.Instance.ClothManager.SaveCurrentClothType(setup.type);
        }

        public void ResetCloth()
        {
            mesh.materials[0].SetTexture(shaderIdName, defaultTexture);
            WorldManager.Instance.ClothManager.SaveCurrentClothType(ClothType.DEFAULT);
        }
    }
}

