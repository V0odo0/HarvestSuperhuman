using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    public class PlantSurfaceProcessor
    {
        public readonly GameProfileData.PlantSurfaceData Data;

        public readonly PlantSlotProcessor[] SlotProcessors;


        public PlantSurfaceProcessor(GameProfileData.PlantSurfaceData data)
        {
            Data = data;

            if (Data.PlantSlots.Count == 0)
                data.PlantSlots.Add(new GameProfileData.PlantSlotData());

            SlotProcessors = new PlantSlotProcessor[GameManager.Data.GameCore.MaxPlantSlots];
            for (int i = 0; i < SlotProcessors.Length; i++)
            {
                
            }

        }

        public void Update()
        {
            foreach (var sp in SlotProcessors) 
            {
                sp.AddGrowthTime(Time.deltaTime);
            }
        }
    }
}
