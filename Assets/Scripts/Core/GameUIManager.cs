using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HSH.UI
{
    public class GameUIManager : UIManager<GameUIManager>
    {
        public UIPlayerHud PlayerHud => _playerHud;
        [SerializeField] private UIPlayerHud _playerHud;
    }
}
