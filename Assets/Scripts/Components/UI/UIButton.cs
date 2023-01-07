using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HSH.UI
{
    public class UIButton : UIMonoBehaviour
    {
        public string Id
        {
            get => _id;
            set => _id = value;
        }
        [SerializeField] private string _id;

        public Button Button => _button;
        public string Label
        {
            get => _labelText == null ? string.Empty : _labelText.text;
            set
            {
                if (_labelText != null)
                    _labelText.text = value;
            }
        }
        public Sprite Icon
        {
            get => _iconImage == null ? null : _iconImage.sprite;
            set
            {
                _iconImage.sprite = value;
                _iconImage.enabled = value != null;
            }
        }
        public Color Color
        {
            get => _button == null ? Color.white : _button.targetGraphic.color;
            set
            {
                if (_button == null || _button.targetGraphic == null)
                    return;

                _button.targetGraphic.color = value;
                if (_labelText != null)
                    _labelText.color = value;
            }
        }


        [Header("Refs")]
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private Image _iconImage;
    }
}
