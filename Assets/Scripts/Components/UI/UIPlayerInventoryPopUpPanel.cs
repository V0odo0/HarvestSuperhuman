using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Knot.Localization;
using UnityEngine;

namespace HSH.UI
{
    public class UIPlayerInventoryPopUpPanel : UIPopUpPanel
    {
        [SerializeField] private UIDnaItemsList _seedDnaItemsList;
        [SerializeField] private UIDnaItemsList _wombDnaItemsList;


        protected override void Awake()
        {
            base.Awake();

            _seedDnaItemsList.Selected += (sender, item) =>
            {
                TryRemoveDnaItem(item);
            };
            _wombDnaItemsList.Selected += (sender, item) =>
            {
                TryRemoveDnaItem(item);
            };
        }

        void TryRemoveDnaItem(UIDnaItem item)
        {
            UIButtonPickerPopUpPanel.Open(string.Empty, i =>
            {
                switch (i)
                {
                    case 0:
                        if (App.ActiveGameProfile.DnaItems.Contains(item.Data))
                        {
                            App.ActiveGameProfile.DnaItems.Remove(item.Data);
                            UpdateList();
                        }
                        break;
                }
            }, KnotLocalization.GetText("UI.DeleteDnaItem"));
        }

        void UpdateList()
        {
            _seedDnaItemsList.Set(App.ActiveGameProfile.DnaItems.Where(d => d.Type == DnaItemType.Seed).ToList());
            _wombDnaItemsList.Set(App.ActiveGameProfile.DnaItems.Where(d => d.Type == DnaItemType.Womb).ToList());
        }

        public override void Show()
        {
            base.Show();

            UpdateList();
        }
    }
}
