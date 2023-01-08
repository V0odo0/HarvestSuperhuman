using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HSH.UI
{
    public class UIModButton : UIButton
    {
        public ModConfigAsset Config { get; private set; }

        [Header("Refs")]
        [SerializeField] private Image _bgImage;


        protected override void Awake()
        {
            base.Awake();
        }


        public void Set(ModConfigAsset config)
        {
            Config = config;
            if (Config == null)
                return;

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
            Label = config.GetName();
        }
    }
}
