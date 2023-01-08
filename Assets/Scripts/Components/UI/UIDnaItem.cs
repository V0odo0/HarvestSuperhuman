using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HSH.UI
{
    public class UIDnaItem : UIMonoBehaviour
    {
        public event EventHandler Selected;

        public GameProfileData.DnaItemData Data { get; private set; }
        public CanvasGroup CanvasGroup => _canvasGroup;
        
        [SerializeField] private bool _allowShowAsNew = true;
        [SerializeField] private Sprite _seedSprite;
        [SerializeField] private Sprite _wombSprite;


        [Header("Refs")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _modsRoot;
        [SerializeField] private GameObject _isNewObj;
        [SerializeField] private Button _selectButton;

        [Space]
        [SerializeField] private Image _dnaTypeImage;
        [Space]
        [SerializeField] private TextMeshProUGUI _vitText;
        [SerializeField] private TextMeshProUGUI _strText;
        [SerializeField] private TextMeshProUGUI _intText;


        private List<UIModButton> _curModButtons = new List<UIModButton>();


        protected override void Awake()
        {
            base.Awake();

            _selectButton.onClick.AddListener(() => Selected?.Invoke(this, EventArgs.Empty));
        }

        public void Set(GameProfileData.DnaItemData data, bool isNew = false)
        {
            _isNewObj.gameObject.SetActive(isNew && _allowShowAsNew);

            Data = data;

            _dnaTypeImage.sprite = data.Type == DnaItemType.Seed ? _seedSprite : _wombSprite;

            _vitText.text = data.Stats.Vit.ToStringNice();
            _strText.text = data.Stats.Str.ToStringNice();
            _intText.text = data.Stats.Int.ToStringNice();


            foreach (var modButton in _curModButtons)
                modButton.gameObject.SetActive(false);

            foreach (var m in data.Mods)
            {
                var modConfig = GameManager.Data.Mods.GetById(m.Id);
                if (modConfig == null)
                    return;

                var modBtn = _curModButtons.FirstOrDefault(b => !b.gameObject.activeSelf);
                if (modBtn == null)
                {
                    modBtn = Instantiate(GameManager.Data.UIPrefabs.ModButtonIconOnly).GetComponent<UIModButton>();
                    modBtn.RectTransform.SetParent(_modsRoot, false);
                    modBtn.Button.onClick.AddListener(() =>
                    {
                        UIManager<GameUIManager>.Instance.GetPopUpPanel<UIModInfoPopUpPanel>().Show(modConfig);
                    });

                    _curModButtons.Add(modBtn);
                }

                modBtn.Set(modConfig);
                modBtn.gameObject.SetActive(true);
            }
        }
    }
}
