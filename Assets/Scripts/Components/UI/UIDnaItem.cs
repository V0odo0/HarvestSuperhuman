using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HSH.UI
{
    public class UIDnaItem : UIMonoBehaviour
    {
        public EventHandler Selected;
        public EventHandler RequestRemove;

        public GameProfileData.DnaItemData Data { get; private set; }

        
        [Header("Refs")]
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _requestRemoveButton;

        [Space]
        [SerializeField] private TextMeshProUGUI _vitText;
        [SerializeField] private TextMeshProUGUI _strText;
        [SerializeField] private TextMeshProUGUI _intText;
        [SerializeField] private TextMeshProUGUI _immText;
        [SerializeField] private TextMeshProUGUI _beaText;


        protected override void Awake()
        {
            base.Awake();

            _selectButton.onClick.AddListener(() => Selected?.Invoke(this, EventArgs.Empty));
            _requestRemoveButton.onClick.AddListener(() => RequestRemove?.Invoke(this, EventArgs.Empty));

        }

        public void Set(GameProfileData.DnaItemData data)
        {
            Data = data;

            _vitText.text = data.Stats.Vit.ToString();
            _strText.text = data.Stats.Str.ToString();
            _intText.text = data.Stats.Int.ToString();
            _immText.text = data.Stats.Imm.ToString();
            _beaText.text = data.Stats.Bea.ToString();
        }
    }
}
