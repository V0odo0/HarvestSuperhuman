using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    [Serializable]
    public class UserLocalGameProfile : UserLocalFileBase<UserLocalGameProfile>
    {
        public GameProfileData GameProfileData => _gameProfileData ?? (_gameProfileData = new GameProfileData());
        [SerializeField] private GameProfileData _gameProfileData;
    }
}
