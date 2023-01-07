using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    public class PlantSlotProcessor : INotifyPropertyChanged
    {
        public event Action<string> PropertyChanged;

        public GameProfileData.PlantSlotData Data { get; private set; }

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

        public int GrowthStage;


        public void SetData(GameProfileData.PlantSlotData data)
        {
            Data = data;

            GrowthTime = data.GrowthTime;
        }

        public void AddGrowthTime(float t)
        {
            switch (State)
            {
                case SlotState.Planted:
                    Data.GrowthTime += Mathf.Clamp(t, 0, float.MaxValue);
                    break;
            }
        }

        public enum SlotState
        {
            None,
            Locked,
            Empty,
            Planted
        }
    }
}
