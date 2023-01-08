using System.Collections;
using System.Collections.Generic;
using Knot.Localization;
using TMPro;
using UnityEngine;

namespace HSH.UI
{
    public class UIModInfoPopUpPanel : UIPopUpPanel
    {
        [Header("Refs")]
        [SerializeField] private UIModButton _modButton;
        [SerializeField] private TextMeshProUGUI _modTypeText;
        [SerializeField] private TextMeshProUGUI _modDescriptionText;


        public void Show(ModConfigAsset modConfig)
        {
            if (modConfig == null)
                return;

            _modButton.Set(modConfig);
            _modDescriptionText.text = modConfig.GetDescription();

            switch (modConfig.Info.Type)
            {
                case ModType.Negative:
                    _modTypeText.text = KnotLocalization.GetText("Mod.Type.Negative");
                    break;
                case ModType.Neutral:
                    _modTypeText.text = KnotLocalization.GetText("Mod.Type.Neutral");
                    break;
                case ModType.Positive:
                    _modTypeText.text = KnotLocalization.GetText("Mod.Type.Positive");
                    break;
            }

            base.Show();
        }
    }
}
