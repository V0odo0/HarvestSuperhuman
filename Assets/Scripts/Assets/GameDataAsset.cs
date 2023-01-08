using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    [CreateAssetMenu(menuName = "HSH/GameDataAsset", fileName = "GameDataAsset")]
    public class GameDataAsset : ScriptableObject
    {
        public GameCoreData GameCore => _gameCore;
        [SerializeField] private GameCoreData _gameCore;


        [Serializable]
        public class GameCoreData
        {
            [field:SerializeField]
            public int MaxPlantSlots = 5;

            [field:SerializeField]
            public int MaxGrowthStages = 10;

            [field: SerializeField]
            public int GrowthStageTime = 2;

            [field: SerializeField]
            public int DefaultStatThreshold = 10;

            [field:SerializeField]
            public GameProfileData.DnaItemData DefaultDnaItem;

        }
    }
}
