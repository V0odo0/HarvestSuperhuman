using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HSH.UI
{
    public class UIDnaStat : UIMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statText;
        [SerializeField] private TextMeshProUGUI _statValueText;

        [SerializeField] private Image _valueDiffImage;
        [SerializeField] private Image _valueFillerImage;


        void SetStatType(StatType type)
        {
            _statText.text = type.ToString().ToUpper();
            _statText.color = GameManager.Data.Colors.GetStatColor(type);
            _statValueText.color = GameManager.Data.Colors.GetStatColor(type);
            _valueFillerImage.color = GameManager.Data.Colors.GetStatColor(type);
        }


        public void Set(int val, int max, StatType type)
        {
            SetStatType(type);

            _statValueText.text = val.ToStringNice();

            var valNorm = (float) val / max;
            _valueFillerImage.fillAmount = Mathf.Clamp01(valNorm);

            _valueDiffImage.enabled = false;
        }

        public void SetDiff(int oldVal, int newVal, int max, StatType type)
        {
            SetStatType(type);

            var valDiff = newVal - oldVal;
            _statValueText.text = valDiff >= 0 ? $"+{valDiff.ToStringNice()}" : valDiff.ToStringNice();

            var valNorm = (float) newVal / max;
            _valueFillerImage.fillAmount = Mathf.Clamp01(valNorm);

            _valueDiffImage.enabled = true;
            var oldValNorm = (float) oldVal / max;
            _valueDiffImage.rectTransform.anchorMin = new Vector2(Mathf.Min(oldValNorm, valNorm), _valueDiffImage.rectTransform.anchorMin.y);
            _valueDiffImage.fillAmount = Mathf.Max(oldValNorm, valNorm) - Mathf.Min(oldValNorm, valNorm);
            _valueDiffImage.color = newVal >= oldVal ? Color.green : Color.red;
        }
    }
}
