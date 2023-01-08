using System;
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
        [SerializeField] private GameObject _isNewObj;
        [SerializeField] private Button _selectButton;

        [Space]
        [SerializeField] private Image _dnaTypeImage;
        [Space]
        [SerializeField] private TextMeshProUGUI _vitText;
        [SerializeField] private TextMeshProUGUI _strText;
        [SerializeField] private TextMeshProUGUI _intText;


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
        }
    }
}
