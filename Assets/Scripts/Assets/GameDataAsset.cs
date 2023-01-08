using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HSH.UI;
using UnityEngine;

namespace HSH
{
    [CreateAssetMenu(menuName = "HSH/GameDataAsset", fileName = "GameDataAsset")]
    public class GameDataAsset : ScriptableObject
    {
        public GameCoreData GameCore => _gameCore;
        [SerializeField] private GameCoreData _gameCore;

        public ColorsCollection Colors => _colors;
        [SerializeField] private ColorsCollection _colors;

        public UIPrefabsCollection UIPrefabs => _uiPrefabs;
        [SerializeField] private UIPrefabsCollection _uiPrefabs;

        public ModsCollection Mods => _mods;
        [SerializeField] private ModsCollection _mods;


        [Serializable]
        public class GameCoreData
        {
            [field:SerializeField]
            public int MaxPlantSlots = 5;

            [field:SerializeField]
            public int MaxGrowthStages = 10;

            [field: SerializeField]
            public float GrowthStageTime = 2;

            [field: SerializeField]
            public int DefaultStatBreedBound = 10;

            [field:SerializeField]
            public GameProfileData.DnaItemData DefaultSeedDnaItem;

            [field: SerializeField]
            public GameProfileData.DnaItemData DefaultWombDnaItem;

        }

        [Serializable]
        public class ModsCollection
        {
            [field: SerializeField]
            public List<ModConfigAsset> All = new List<ModConfigAsset>();


            public ModConfigAsset GetById(string id)
            {
                return All.FirstOrDefault(a => a.Info.Id == id);
            }
        }

        [Serializable]
        public class ColorsCollection
        {
            [field: SerializeField]
            public Color NegativeLight { get; private set; }

            [field: SerializeField]
            public Color NeutralLight { get; private set; }

            [field: SerializeField]
            public Color PositiveLight { get; private set; }

        }

        [Serializable]
        public class UIPrefabsCollection
        {
            [field:SerializeField]
            public UIModButton ModButtonIconOnly { get; private set; }
            
            [field: SerializeField]
            public UIModButton ModButton { get; private set; }
        }
    }

    [Serializable]
    public enum StatType
    {
        Vit,
        Str,
        Int
    }
}
