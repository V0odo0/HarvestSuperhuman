using System;
using System.Collections;
using System.Collections.Generic;
using HSH.UI;
using UnityEngine;

namespace HSH
{
    public class PlantSlot : MonoBehaviourBase
    {
        public event EventHandler<PlantSlotProcessor.SlotState> StateActionRequested;

        public int Id => _id;
        [SerializeField] private int _id;

        public PlantSlotProcessor Processor { get; private set; }


        [SerializeField] private Sprite _lockedStateActionIcon;
        [SerializeField] private Sprite _emptyStateActionIcon;
        [SerializeField] private Sprite _plantedStateActionIcon;

        [Header("Refs")]
        [SerializeField] private GameObject _root;
        [SerializeField] private UIButton _actionButton;
        [SerializeField] private CanvasGroup _actionButtonCanvasGroup;
        

        protected override void Awake()
        {
            base.Awake();
            
            _actionButton.Button.onClick.AddListener(() =>
            {
                if (Processor != null)
                    StateActionRequested?.Invoke(this, Processor.State);
            });
        }


        void SetState(PlantSlotProcessor.SlotState state)
        {
            _root.SetActive(state != PlantSlotProcessor.SlotState.None);
            
            switch (state)
            {
                case PlantSlotProcessor.SlotState.Locked:
                    _actionButton.Button.interactable = false;
                    _actionButton.Icon = _lockedStateActionIcon;
                    break;
                case PlantSlotProcessor.SlotState.Empty:
                    _actionButton.Button.interactable = true;
                    _actionButton.Icon = _emptyStateActionIcon;
                    break;
                case PlantSlotProcessor.SlotState.Planted:
                    _actionButton.Button.interactable = true;
                    _actionButton.Icon = _plantedStateActionIcon;
                    break;
            }

            _actionButtonCanvasGroup.alpha = _actionButton.Button.interactable ? 1f : 0.5f;
        }

        public void SetProcessor(PlantSlotProcessor processor)
        {
            if (processor == null)
            {
                Processor = null;
                SetState(PlantSlotProcessor.SlotState.None);
                return;
            }

            Processor = processor;

            SetState(Processor.State);
        }
    }
}
