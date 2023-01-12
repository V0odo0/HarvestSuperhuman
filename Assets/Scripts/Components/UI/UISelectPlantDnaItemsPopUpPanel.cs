using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HSH.UI
{
    public class UISelectPlantDnaItemsPopUpPanel : UIPopUpPanel
    {
        public GameProfileData.DnaItemData SelectedSeed { get; private set; }
        public GameProfileData.DnaItemData SelectedWomb { get; private set; }

        [SerializeField] private UIDnaItemsList _seedDnaItemsList;
        [SerializeField] private UIDnaItemsList _wombDnaItemsList;

        [SerializeField] private UIDnaItem _selectedSeedItem;
        [SerializeField] private Animation _selectedSeedItemAnim;
        [SerializeField] private UIDnaItem _selectedWombItem;
        [SerializeField] private Animation _selectedWombItemAnim;
        

        [SerializeField] private UIButton _plantButton;


        private Action _plantClicked;


        protected override void Awake()
        {
            base.Awake();

            _seedDnaItemsList.Selected += (sender, item) =>
            {
                SelectedSeed = item.Data;
                UpdateSelection();
                if (SelectedSeed != null)
                {
                    _selectedSeedItemAnim.Play();
                    SoundManager.Play(GameManager.Data.Sounds.BlopA, 0.5f, 1.1f);
                }
            };
            _wombDnaItemsList.Selected += (sender, item) =>
            {
                SelectedWomb = item.Data;
                UpdateSelection();

                if (SelectedWomb != null)
                {
                    _selectedWombItemAnim.Play();
                    SoundManager.Play(GameManager.Data.Sounds.BlopA, 0.5f, 1.1f);
                }
            };

            _selectedSeedItem.Selected += (sender, args) =>
            {
                SelectedSeed = null;
                UpdateSelection();
                SoundManager.Play(GameManager.Data.Sounds.BlopA, 0.5f, 0.7f);
            };
            _selectedWombItem.Selected += (sender, args) =>
            {
                SelectedWomb = null;
                UpdateSelection();
                SoundManager.Play(GameManager.Data.Sounds.BlopA, 0.5f, 0.7f);
            };

            _plantButton.Button.onClick.AddListener(() =>
            {
                if (SelectedWomb == null || SelectedSeed == null)
                    return;

                _plantClicked?.Invoke();
                Hide();
            });
        }


        void UpdateSelection()
        {
            _selectedSeedItem.gameObject.SetActive(SelectedSeed != null);
            if (SelectedSeed != null)
                _selectedSeedItem.Set(SelectedSeed);

            _selectedWombItem.gameObject.SetActive(SelectedWomb != null);
            if (SelectedWomb != null)
                _selectedWombItem.Set(SelectedWomb);

            foreach (var item in _seedDnaItemsList.ActiveItems)
                item.CanvasGroup.interactable = SelectedSeed != item.Data;

            foreach (var item in _wombDnaItemsList.ActiveItems)
                item.CanvasGroup.interactable = SelectedWomb != item.Data;

            _plantButton.Button.interactable = SelectedWomb != null && SelectedSeed != null;
        }

        public void Show(Action plantClicked)
        {
            base.Show();
            
            _plantClicked = plantClicked;
            SelectedSeed = null;
            SelectedWomb = null;

            UpdateSelection();

            var seeds = new List<GameProfileData.DnaItemData>(App.ActiveGameProfile.DnaItems.Where(data => data.Type == DnaItemType.Seed));
            seeds.Insert(0, GameManager.Data.GameCore.DefaultSeedDnaItem);
            _seedDnaItemsList.Set(seeds);

            var wombs = new List<GameProfileData.DnaItemData>(App.ActiveGameProfile.DnaItems.Where(data => data.Type == DnaItemType.Womb));
            wombs.Insert(0, GameManager.Data.GameCore.DefaultWombDnaItem);
            _wombDnaItemsList.Set(wombs);
        }
    }
}
