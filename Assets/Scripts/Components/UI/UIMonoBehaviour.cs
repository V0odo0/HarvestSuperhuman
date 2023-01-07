using UnityEngine;

namespace HSH.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIMonoBehaviour : MonoBehaviourBase
    {
        public RectTransform RectTransform => _rectTransform == null ? _rectTransform = GetComponent<RectTransform>() : _rectTransform;
        private RectTransform _rectTransform;
    }
}