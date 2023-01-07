using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public static GameDataAsset GameData =>
            _gameData ?? (_gameData = Resources.Load<GameDataAsset>(nameof(GameDataAsset)));
        private static GameDataAsset _gameData;
    }
}
