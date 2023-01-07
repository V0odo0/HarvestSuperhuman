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
            
        }


        public void SetProcessor(PlantSurfaceProcessor processor)
        {
            Processor = processor;
        }
    }
}
