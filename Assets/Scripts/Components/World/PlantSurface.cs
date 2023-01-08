using System;
using System.Collections;
using System.Collections.Generic;
using HSH.UI;
using UnityEngine;
using UnityEngine.U2D;

namespace HSH
{
    public class PlantSurface : MonoBehaviourBase
    {
        public PlantSurfaceProcessor Processor { get; private set; }

        public PlantSlot[] Slots => _slots;

        [Header("Refs")]
        [SerializeField] private PlantSlot[] _slots;
        [SerializeField] private SpriteShapeController _surfaceShape;


        protected override void Awake()
        {
            base.Awake();

            foreach (var slot in _slots)
            {
                slot.StateActionRequested += SlotOnStateActionRequested;
            }
        }

        void SlotOnStateActionRequested(object sender, PlantSlotProcessor.SlotState e)
        {
            if (!(sender is PlantSlot slot))
                return;

            switch (e)
            {
                case PlantSlotProcessor.SlotState.Empty:
                    GameUIManager.Instance.GetPopUpPanel<UISelectPlantDnaItemsPopUpPanel>().Show(() =>
                    {
                        var panel = GameUIManager.Instance.GetPopUpPanel<UISelectPlantDnaItemsPopUpPanel>();
                        if (App.ActiveGameProfile.DnaItems.Contains(panel.SelectedWomb))
                            App.ActiveGameProfile.DnaItems.Remove(panel.SelectedWomb);

                        if (App.ActiveGameProfile.DnaItems.Contains(panel.SelectedSeed))
                            App.ActiveGameProfile.DnaItems.Remove(panel.SelectedSeed);

                        slot.Processor.Plant(panel.SelectedSeed, panel.SelectedWomb);
                    });

                    break;
                case PlantSlotProcessor.SlotState.FullyGrown:
                    var result = slot.Processor.FetchBreedResult();
                    App.ActiveGameProfile.DnaItems.Add(result);
                    GameUIManager.Instance.PlayerHud.ShakeInventoryButton();
                    break;
            }
        }


        public void SetProcessor(PlantSurfaceProcessor processor)
        {
            Processor = processor;

            for (int i = 0; i < Mathf.Min(_slots.Length, Processor.SlotProcessors.Length); i++)
            {
                _slots[i].SetProcessor(Processor.SlotProcessors[i]);
            }
        }
    }
}
