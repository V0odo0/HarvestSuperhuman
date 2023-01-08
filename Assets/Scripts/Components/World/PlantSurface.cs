using System;
using System.Collections;
using System.Collections.Generic;
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
                    var seed = GameManager.Data.GameCore.DefaultDnaItem;
                    var womb = GameManager.Data.GameCore.DefaultDnaItem;

                    slot.Processor.Plant(seed, womb);
                    break;
                case PlantSlotProcessor.SlotState.FullyGrown:
                    var result = slot.Processor.GetBreedResult();
                    if (result.Type == DnaItemType.Seed)
                        App.ActiveGameProfile.Seeds.Add(result);
                    else App.ActiveGameProfile.Wombs.Add(result);
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
