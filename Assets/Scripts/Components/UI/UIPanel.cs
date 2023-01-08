using System;
using UnityEngine;
using Random = UnityEngine.Random;


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

            if (Settings.PlayOpenAudio)
                SoundManager.Play(GameManager.Data.Sounds.StretchA, 0.05f, Random.Range(1.25f, 1.35f));
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

            public bool PlayOpenAudio => _playOpenAudio;
            [SerializeField] private bool _playOpenAudio = true;
        }
    }
}
