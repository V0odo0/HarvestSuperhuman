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

        [SerializeField] private UIPopUpPanel _isThatItPopUpPanel;
        [SerializeField] private UIButton _isThatItButton;


        protected override void Awake()
        {
            base.Awake();

            _isThatItButton.Button.onClick.AddListener(() => _isThatItPopUpPanel.Show());
            _isThatItButton.gameObject.SetActive(false);

            StartCoroutine(IsThatItYield());
        }


        IEnumerator IsThatItYield()
        {
            yield return new WaitForSeconds(25);

            SoundManager.Play(GameManager.Data.Sounds.BlopA);
            _isThatItButton.gameObject.SetActive(true);
        }
    }
}
