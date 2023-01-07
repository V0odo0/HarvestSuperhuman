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
            
        }
    }
}
