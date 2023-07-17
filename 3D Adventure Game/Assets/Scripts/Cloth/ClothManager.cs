using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cloth
{
    public enum ClothType
    {
        SPEED
    }

    public class ClothManager : MonoBehaviour
    {
        public List<ClothSetup> clothSetup = new List<ClothSetup>();

        public void GetClothByType(ClothType clothType)
        {
            clothSetup.Find(x => x.type == clothType);
        }

        [System.Serializable]
        public class ClothSetup
        {
            public ClothType type;
            public Texture texture;
        }
    }
}

