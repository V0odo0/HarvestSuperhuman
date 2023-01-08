using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HSH.UI
{
    public class UIModButton : UIButton
    {
        [Header("Refs")]
        [SerializeField] private Image _bgImage;
        [SerializeField] private TextMeshProUGUI _nameText;


        public void Set(ModConfigAsset config)
        {
            if (_bgImage != null)
            {
                switch (config.Info.Type)
                {
                    case ModType.Negative:
                        _bgImage.color = GameManager.Data.Colors.NegativeLight;
                        break;
                    case ModType.Neutral:
                        _bgImage.color = GameManager.Data.Colors.NeutralLight;
                        break;
                    case ModType.Positive:
                        _bgImage.color = GameManager.Data.Colors.PositiveLight;
                        break;
                }
            }

            Icon = config.Info.Icon;

            if (_nameText != null)
                _nameText.text = config.GetName();
        }
    }
}
