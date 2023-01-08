using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HSH
{
    public class BreedProcessor
    {
        public readonly GameProfileData.DnaItemData Seed;
        public readonly GameProfileData.DnaItemData Womb;


        public readonly StatProcessor VitProcessor;
        public readonly StatProcessor StrProcessor;
        public readonly StatProcessor IntProcessor;

        public readonly List<ModConfigAsset> SeedBaseMods;
        public readonly List<ModConfigAsset> WombBaseMods;
        public readonly List<ModConfigAsset> ResultMods;

        public readonly HashSet<string> Tags = new HashSet<string>();

        public readonly Dictionary<StatType, StatProcessor> AllStatProcessors;
        

        private GameProfileData.DnaItemData _result;
        

        public BreedProcessor(GameProfileData.DnaItemData seed, GameProfileData.DnaItemData womb)
        {
            Seed = seed;
            Womb = womb;

            SeedBaseMods = seed.Mods.Select(d => GameManager.Data.Mods.GetById(d.Id)).Where(d => d != null).ToList();
            WombBaseMods = seed.Mods.Select(d => GameManager.Data.Mods.GetById(d.Id)).Where(d => d != null).ToList();
            ResultMods = new List<ModConfigAsset>();

            VitProcessor = new StatProcessor(Mathf.Min(seed.Stats.Vit, womb.Stats.Vit), Mathf.Max(seed.Stats.Vit, womb.Stats.Vit));
            StrProcessor = new StatProcessor(Mathf.Min(seed.Stats.Str, womb.Stats.Str), Mathf.Max(seed.Stats.Str, womb.Stats.Str));
            IntProcessor = new StatProcessor(Mathf.Min(seed.Stats.Int, womb.Stats.Int), Mathf.Max(seed.Stats.Int, womb.Stats.Int));
            AllStatProcessors = new Dictionary<StatType, StatProcessor>()
            {
                { StatType.Vit, VitProcessor },
                { StatType.Str, StrProcessor },
                { StatType.Int, IntProcessor }
            };

            foreach (var p in AllStatProcessors.Values)
            {
                p.MinBound = GameManager.Data.GameCore.DefaultStatBreedBound;
                p.MaxBound = GameManager.Data.GameCore.DefaultStatBreedBound;
            }

            _result = new GameProfileData.DnaItemData();
            _result.Type = Random.Range(0, 2) == 0 ? DnaItemType.Seed : DnaItemType.Womb;

            foreach (var m in GameManager.Data.Mods.All)
                m.Process(this);

            ProcessStats(_result);

            for (int i = 0; i < Mathf.Min(GameManager.Data.GameCore.MaxDnaMods, ResultMods.Count); i++)
                _result.Mods.Add(ResultMods[i].ToData());
        }


        void ProcessStats(GameProfileData.DnaItemData data)
        {
            data.Stats.Vit = VitProcessor.Generate();
            data.Stats.Str = StrProcessor.Generate();
            data.Stats.Int = IntProcessor.Generate();
        }


        public GameProfileData.DnaItemData GetBreedResult()
        {
            return _result;
        }


        public class StatProcessor
        {
            public int Min;
            public int Max;
            public int Avg => (Max + Min) / 2;


            public int MinBound;
            public int MaxBound;

            public float MinMaxDeviation
            {
                get => _minMaxDeviation;
                set => _minMaxDeviation = Mathf.Clamp01(value);
            }
            private float _minMaxDeviation;



            public StatProcessor(int min, int max)
            {
                Min = min;
                Max = max;
                _minMaxDeviation = Random.Range(0f, 1f);
            }
            

            public int Generate()
            {
                return Mathf.FloorToInt(Mathf.Lerp(Min - MinBound, Max + MaxBound, MinMaxDeviation));
            }
        }
    }
}
