using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HSH.UI
{
    public class UIModInfoPopUpPanel : UIPopUpPanel
    {
        [Header("Refs")]
        [SerializeField] private UIModButton _modButton;
        [SerializeField] private TextMeshProUGUI _modDescriptionText;


        public void Show(ModConfigAsset modConfig)
        {
            if (modConfig == null)
                return;

            _modButton.Set(modConfig);
            _modDescriptionText.text = modConfig.GetDescription();

            base.Show();
        }
    }
}
