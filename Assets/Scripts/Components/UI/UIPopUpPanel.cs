using System;
using System.Linq;
using UnityEngine;

namespace HSH.UI
{
    public class UIPopUpPanel : UIPanel
    {
        public static event Action<UIPopUpPanel, bool> PopUpPanelSwitch;

        public virtual bool CanBeClosedByHotKey => PopUpSettings.CanBeClosedByHotKey;


        public PopUpPanelSettings PopUpSettings => _popUpSettings;
        [SerializeField] private PopUpPanelSettings _popUpSettings;


        [Header("Refs")]
        [SerializeField] protected Animation _popUpAnimation;


        protected override void OnEnable()
        {
            UIManager<GameUIManager>.ActivePopUpPanels.Add(this);

            base.OnEnable();
        }

        protected override void OnDisable()
        {
            UIManager<GameUIManager>.ActivePopUpPanels.Remove(this);

            base.OnDisable();
        }


        public override void Show()
        {
            RectTransform.SetAsLastSibling();

            base.Show();

            if (_popUpAnimation != null)
                _popUpAnimation.Play();

            PopUpPanelSwitch?.Invoke(this, true);
        }

        public override void Hide()
        {
            base.Hide();

            PopUpPanelSwitch?.Invoke(this, false);
        }

        public virtual void CloseByHotKey()
        {
            if (!CanBeClosedByHotKey)
                return;

            Hide();
        }


        [Serializable]
        public class PopUpPanelSettings
        {
            public bool CanBeClosedByHotKey => _canBeClosedByHotKey;
            [SerializeField] private bool _canBeClosedByHotKey = true;
        }
    }
}
