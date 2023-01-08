using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH.UI
{
    public class UIPlayerHud : UIMonoBehaviour
    {
        [SerializeField] private UIButton _openPlayerInventoryButton;
        [SerializeField] private Animation _playerInventoryShakeAnim;


        protected override void Awake()
        {
            base.Awake();

            _openPlayerInventoryButton.Button.onClick.AddListener(() =>
            {
                GameUIManager.Instance.GetPopUpPanel<UIPlayerInventoryPopUpPanel>().Show();
            });
        }

        public void ShakeInventoryButton()
        {
            _playerInventoryShakeAnim.Play();
        }
    }
}
