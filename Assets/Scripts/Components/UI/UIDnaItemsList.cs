using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HSH.UI
{
    public class UIDnaItemsList : UIMonoBehaviour
    {
        private static List<GameProfileData.DnaItemData> _newDnaItems = new List<GameProfileData.DnaItemData>();

        public event EventHandler<UIDnaItem> Selected;

        public IEnumerable<UIDnaItem> ActiveItems => _curItems.Where(i => i.gameObject.activeSelf);

        [SerializeField] private UIDnaItem _itemSrc;
        [SerializeField] private RectTransform _listRoot;

        private List<UIDnaItem> _curItems = new List<UIDnaItem>();


        protected override void OnDisable()
        {
            base.OnDisable();

            foreach (var i in ActiveItems)
            {
                if (!_newDnaItems.Contains(i.Data))
                    _newDnaItems.Add(i.Data);
            }
        }


        void OnSelected(object sender, EventArgs e)
        {
            if (!(sender is UIDnaItem d))
                return;

            Selected?.Invoke(this, d);
        }

        public void Set(IList<GameProfileData.DnaItemData> items)
        {
            foreach (var item in _curItems)
                item.gameObject.SetActive(false);

            for (int i = 0; i < items.Count; i++)
            {
                var item = _curItems.FirstOrDefault(item => !item.gameObject.activeSelf);
                if (item == null)
                {
                    item = Instantiate(_itemSrc.gameObject).GetComponent<UIDnaItem>();
                    item.RectTransform.SetParent(_listRoot, false);
                    item.Selected += OnSelected;

                    _curItems.Add(item);
                }

                var isNew = !_newDnaItems.Contains(items[i]);
                item.Set(items[i], isNew);
                item.RectTransform.SetAsLastSibling();
                item.gameObject.SetActive(true);
            }
        }
    }
}
