using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cloth
{
    public enum ClothType
    {
        SPEED,
        STRONG
    }

    public class ClothManager : MonoBehaviour
    {
        public List<ClothSetup> clothSetup = new List<ClothSetup>();

        public ClothSetup GetClothByType(ClothType clothType)
        {
            return clothSetup.Find(x => x.type == clothType);
        }      
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType type;
        public Texture texture;
    }
}

