using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace HSH
{
    public class PlantSlotProcessor : INotifyPropertyChanged
    {
        public event Action<string> PropertyChanged;

        public GameProfileData.PlantSlotData Data { get; private set; }

        public BreedProcessor BreedProcessor
        {
            get => _breedProcessor;
            private set
            {
                if (_breedProcessor == value)
                    return;

                _breedProcessor = value;
                PropertyChanged?.Invoke(nameof(BreedProcessor));
            }
        }
        private BreedProcessor _breedProcessor;

        public SlotState State
        {
            get => _state;
            private set
            {
                if (_state == value)
                    return;

                _state = value;
                PropertyChanged?.Invoke(nameof(State));
            }
        }
        private SlotState _state;

        public float GrowthTime { get; private set; }
        
        public int GrowthStage
        {
            get => _growthStage;
            set
            {
                if (_growthStage == value)
                    return;

                _growthStage = value;
                PropertyChanged?.Invoke(nameof(GrowthStage));
            }
        }
        private int _growthStage;

        private GameProfileData.DnaItemData _breedResult;


        void ProcessGrowthStage(int prevStage, int nextStage)
        {

        }

        public void AddGrowthTime(float t)
        {
            if (State != SlotState.Breeding)
                return;

            var targetGrowthTime = GrowthTime + t;
            GrowthTime = targetGrowthTime;
            Data.GrowthTime = GrowthTime;

            while (GrowthStage < Mathf.FloorToInt(GrowthTime / GameManager.Data.GameCore.GrowthStageTime))
            {
                GrowthStage++;
                ProcessGrowthStage(GrowthStage - 1, GrowthStage);

                if (GrowthStage >= GameManager.Data.GameCore.MaxGrowthStages)
                {
                    Data.SeedDna = null;
                    Data.WombDna = null;
                    Data.BreedResultDna = BreedProcessor.GetBreedResult();
                    State = SlotState.FullyGrown;
                    break;
                }
            }
        }

        public void Plant(GameProfileData.DnaItemData seed, GameProfileData.DnaItemData womb)
        {
            if (seed == null || womb == null)
                return;

            _breedResult = null;

            GrowthStage = 0;
            GrowthTime = 0;

            Data.SeedDna = seed;
            Data.WombDna = womb;

            BreedProcessor = new BreedProcessor(seed, womb);
            _breedResult = BreedProcessor.GetBreedResult();

            State = SlotState.Breeding;
        }

        public GameProfileData.DnaItemData FetchBreedResult()
        {
            if (State != SlotState.FullyGrown)
                return null;
            
            GrowthStage = 0;
            GrowthTime = 0;

            Data.SeedDna = null;
            Data.WombDna = null;
            Data.BreedResultDna = null;

            State = SlotState.Empty;

            var result = _breedResult;
            _breedResult = null;
            return result;
        }

        public void SetData(GameProfileData.PlantSlotData data)
        {
            Data = data;

            GrowthTime = data.GrowthTime;

            State = SlotState.Empty;
        }


        [Serializable]
        public enum SlotState
        {
            None = 0,
            Locked = 1,
            Empty = 2,
            Breeding = 3,
            FullyGrown = 4
        }
    }

    [Serializable]
    public enum DnaItemType
    {
        Seed, Womb
    }
}
