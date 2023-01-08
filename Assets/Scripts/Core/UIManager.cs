using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HSH.UI
{
    public abstract class UIManager<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        public static List<UIPopUpPanel> ActivePopUpPanels
        {
            get
            {
                if (_activePopUpPanels.Contains(null))
                    _activePopUpPanels = _activePopUpPanels.Where(p => p != null).ToList();
                return _activePopUpPanels;
            }
        }
        private static List<UIPopUpPanel> _activePopUpPanels = new List<UIPopUpPanel>();

        public IReadOnlyList<UIPopUpPanel> PopUpPanels => _popUpPanels;
        [SerializeField] private List<UIPopUpPanel> _popUpPanels;

        [SerializeField] private Camera _uiCamera;


        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var p = ActivePopUpPanels.LastOrDefault(p => p.CanBeClosedByHotKey);
                if (p != null)
                    p.CloseByHotKey();
            }
        }

        public T GetPopUpPanel<T>() where T : UIPopUpPanel
        {
            return PopUpPanels.OfType<T>().FirstOrDefault();
        }
    }
}
