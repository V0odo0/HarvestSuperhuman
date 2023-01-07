using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanelBase : UIMonoBehaviour
    {
        public CanvasGroup CanvasGroup => _canvasGroup ?? (_canvasGroup = GetComponent<CanvasGroup>());
        private CanvasGroup _canvasGroup;


        public void Open()
        {

        }

        public void Close()
        {

        }
    }
}
