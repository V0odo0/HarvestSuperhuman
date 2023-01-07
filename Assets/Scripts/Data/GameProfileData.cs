using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    [Serializable]
    public class GameProfileData
    {
        public PlantSurfaceData PlantSurface => _plantSurface ?? (_plantSurface = new PlantSurfaceData());
        private PlantSurfaceData _plantSurface = new PlantSurfaceData();

        public List<DnaItemData> Seeds => _seeds ?? (_seeds = new List<DnaItemData>());
        [SerializeField] private List<DnaItemData> _seeds = new List<DnaItemData>();

        public List<DnaItemData> Wombs => _wombs ?? (_wombs = new List<DnaItemData>());
        [SerializeField] private List<DnaItemData> _wombs = new List<DnaItemData>();


        [Serializable]
        public class PlantSurfaceData
        {
            public List<PlantSlotData> PlantSlots => _plantSlots ?? (_plantSlots = new List<PlantSlotData>());
            [SerializeField] private List<PlantSlotData> _plantSlots = new List<PlantSlotData>();
        }

        [Serializable]
        public class PlantSlotData
        {
            public int SlotId;
            public DnaItemData SeedDna;
            public DnaItemData WombDna;
            public float GrowthTime;
        }

        [Serializable]
        public class DnaItemData
        {
            public DnaStatsData Stats => _stats ?? (_stats = new DnaStatsData());
            [SerializeField] private DnaStatsData _stats = new DnaStatsData();

            public List<ModData> Mods => _mods ?? (_mods = new List<ModData>());
            [SerializeField] private List<ModData> _mods = new List<ModData>();
        }

        [Serializable]
        public class DnaStatsData
        {
            public int Vit;
            public int Str;
            public int Int;
            public int Imm;
        }

        [Serializable]
        public class ModData
        {
            public string Guid;
            public int LevelId;
        }
    }
}
