using System;
using UnityEngine;


namespace HSH.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : UIMonoBehaviour
    {
        public CanvasGroup CanvasGroup =>
            _canvasGroup == null ? _canvasGroup = GetComponent<CanvasGroup>() : _canvasGroup;
        private CanvasGroup _canvasGroup;

        public PanelSettings Settings => _settings;
        [SerializeField] private PanelSettings _settings;


        private bool _isFirstTimeAwake = true;

        protected override void Awake()
        {
            if (Settings.HideOnAwake && _isFirstTimeAwake)
                gameObject.SetActive(false);
        }

        
        public virtual void Show()
        {
            _isFirstTimeAwake = false;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            _isFirstTimeAwake = false;
            gameObject.SetActive(false);
        }


        [Serializable]
        public class PanelSettings
        {
            public bool HideOnAwake => _hideOnAwake;
            [SerializeField] private bool _hideOnAwake = true;
        }
    }
}
