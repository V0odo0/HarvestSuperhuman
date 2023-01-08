using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HSH.UI
{
    public class UIDnaItemsList : UIMonoBehaviour
    {
        [SerializeField] private UIDnaItem _itemSrc;
        [SerializeField] private RectTransform _listRoot;

        private List<UIDnaItem> _curItems = new List<UIDnaItem>();



        void Selected(object sender, EventArgs e)
        {
         
        }

        void RequestRemove(object sender, EventArgs e)
        {

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
                    item.RequestRemove += RequestRemove;
                    item.Selected += Selected;

                    _curItems.Add(item);
                }

                item.Set(items[i]);
                item.RectTransform.SetAsLastSibling();
                item.gameObject.SetActive(true);
            }
        }
    }
}
