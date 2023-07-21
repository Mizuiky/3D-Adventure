using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cloth
{
    public enum ClothType
    {
        DEFAULT,
        BASE,
        SPEED,
        STRONG
    }

    public class ClothManager : MonoBehaviour
    {
        public List<ClothSetup> clothSetup = new List<ClothSetup>();

        private  ClothSetup _currentClothType;

        public ClothSetup CurrentCloth
        {
            get
            {
                return _currentClothType;
            }
        }

        public void Awake()
        {
            SaveManager.Instance.fileLoaded += SetCurrentClothFromSaveFile;
        }

        private void OnDestroy()
        {
            SaveManager.Instance.fileLoaded -= SetCurrentClothFromSaveFile;
        }

        public ClothSetup GetClothByType(ClothType clothType)
        {
            return clothSetup.Find(x => x.type == clothType);
        }      

        public void SaveCurrentClothType(ClothType type)
        {
            _currentClothType.type = type;
            SaveManager.Instance.SaveClothType(type);
        }

        public void SetCurrentClothFromSaveFile(SaveSetup setup)
        {
            var cloth = GetClothByType(setup.clothType);
            _currentClothType = cloth;
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType type;
        public Texture texture;
    }
}

