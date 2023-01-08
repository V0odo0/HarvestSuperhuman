using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HSH.UI
{
    public class UIButtonPickerPopUpPanel : UIPopUpPanel
    {
        [Header("Config")]
        [SerializeField] private float _contentMaxHeight = 400;
        [SerializeField] private UIButton _buttonSrc;

        [Header("Refs")]
        [SerializeField] private RectTransform _panelRoot;
        [SerializeField] private RectTransform _contentRoot;
        [SerializeField] private TextMeshProUGUI _titleText;

        private List<UIButton> _curButtons = new List<UIButton>();


        UIButton GetNewButton()
        {
            var button = Instantiate(_buttonSrc.gameObject).GetComponent<UIButton>();
            button.RectTransform.SetParent(_contentRoot, false);

            _curButtons.Add(button);
            return button;
        }

        IEnumerator AdjustContentYield()
        {
            yield return new WaitForEndOfFrame();

            LayoutRebuilder.ForceRebuildLayoutImmediate(_panelRoot);
            _panelRoot.sizeDelta = new Vector2(_panelRoot.sizeDelta.x, Mathf.Min(_contentMaxHeight, _contentRoot.rect.height + 8));


            _contentRoot.gameObject.SetActive(false);
            yield return new WaitForEndOfFrame();
            _contentRoot.gameObject.SetActive(true);
        }

        public void Show(string titleText, Action<int> buttonPicked, Sprite icon, params string[] buttons)
        {
            Show(titleText, buttonPicked, buttons.Select(s => new ButtonSettings(s, true, icon)).ToArray());
        }

        public void Show(string titleText, Action<int> buttonPicked, params ButtonSettings[] buttons)
        {
            if (buttons == null || buttons.Length == 0)
                return;

            _titleText.gameObject.SetActive(!string.IsNullOrEmpty(titleText));
            _titleText.text = titleText;

            _curButtons = _curButtons.Where(b => b != null).ToList();
            foreach (var b in _curButtons)
                b.gameObject.SetActive(false);

            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] == null)
                    continue;

                var button = _curButtons.FirstOrDefault(b => !b.gameObject.activeSelf);
                if (button == null)
                    button = GetNewButton();

                button.Label = buttons[i].Text;
                button.Icon = buttons[i].Icon;
                button.Button.interactable = buttons[i].Interactable;

                var buttonId = i;
                button.Button.onClick.RemoveAllListeners();
                button.Button.onClick.AddListener(() =>
                {
                    Hide();
                    buttonPicked?.Invoke(buttonId);
                });

                button.RectTransform.SetAsLastSibling();
                button.gameObject.SetActive(true);
            }

            _titleText.rectTransform.SetAsFirstSibling();

            base.Show();

            StartCoroutine(AdjustContentYield());
        }

        public static void Open(string titleText, Action<int> buttonPicked, Sprite icon, params string[] buttons)
        {
            var panel = UIManager<GameUIManager>.Instance.GetPopUpPanel<UIButtonPickerPopUpPanel>();
            if (panel == null || buttons.Length == 0)
                return;

            panel.Show(titleText, buttonPicked, icon, buttons);
        }

        public static void Open(string titleText, Action<int> buttonPicked, params string[] buttons)
        {
            var panel = UIManager<GameUIManager>.Instance.GetPopUpPanel<UIButtonPickerPopUpPanel>();
            if (panel == null || buttons.Length == 0)
                return;

            panel.Show(titleText, buttonPicked, null, buttons);
        }

        public static void Open(string titleText, Action<int> buttonPicked, params ButtonSettings[] buttons)
        {
            var panel = UIManager<GameUIManager>.Instance.GetPopUpPanel<UIButtonPickerPopUpPanel>();
            if (panel == null || buttons.Length == 0)
                return;

            panel.Show(titleText, buttonPicked, buttons);
        }


        public class ButtonSettings
        {
            public string Text;
            public bool Interactable = true;
            public Sprite Icon;


            public ButtonSettings() { }

            public ButtonSettings(string text, bool interactable = true, Sprite icon = null)
            {
                Text = text;
                Interactable = interactable;
                Icon = icon;
            }
        }
    }
}
