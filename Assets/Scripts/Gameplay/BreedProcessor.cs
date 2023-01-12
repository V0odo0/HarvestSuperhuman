using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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
            WombBaseMods = womb.Mods.Select(d => GameManager.Data.Mods.GetById(d.Id)).Where(d => d != null).ToList();
            ResultMods = new List<ModConfigAsset>();


            var defaultMinMaxBreedBound = GameManager.Data.GameCore.DefaultMinMaxBreedBound;
            var defaultDeviation = GameManager.Data.GameCore.DefaultMinMaxDeviation;

            VitProcessor = new StatProcessor(new Vector2Int(Mathf.Min(seed.Stats.Vit, womb.Stats.Vit),
                Mathf.Max(seed.Stats.Vit, womb.Stats.Vit)), defaultMinMaxBreedBound, defaultDeviation);
            StrProcessor = new StatProcessor(new Vector2Int(Mathf.Min(seed.Stats.Str, womb.Stats.Str), 
                Mathf.Max(seed.Stats.Str, womb.Stats.Str)), defaultMinMaxBreedBound, defaultDeviation);
            IntProcessor = new StatProcessor(new Vector2Int(Mathf.Min(seed.Stats.Int, womb.Stats.Int), 
                Mathf.Max(seed.Stats.Int, womb.Stats.Int)), defaultMinMaxBreedBound, defaultDeviation);

            AllStatProcessors = new Dictionary<StatType, StatProcessor>
            {
                { StatType.Vit, VitProcessor },
                { StatType.Str, StrProcessor },
                { StatType.Int, IntProcessor }
            };
            

            _result = new GameProfileData.DnaItemData();
            _result.Type = Random.Range(0f, 1f) > 0.5f ? DnaItemType.Seed : DnaItemType.Womb;

            foreach (var m in GameManager.Data.Mods.Selection)
                m.TrySelect(this);

            foreach (var m in ResultMods.ToArray())
            {
                if (ResultMods.Contains(m))
                    m.Process(this);
            }

            foreach (var m in ResultMods.ToArray())
            {
                if (!ResultMods.Contains(m))
                    continue;

                m.Apply(this);
            }

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
            public Vector2Int MinMax;
            public int Avg => (MinMax.x + MinMax.y) / 2;
            public int AbsAdd;


            public Vector2Int MinMaxBreedBound;

            public float MinMaxDeviation
            {
                get => _minMaxDeviation;
                set => _minMaxDeviation = Mathf.Clamp01(value);
            }
            private float _minMaxDeviation;



            public StatProcessor(Vector2Int minMax, Vector2Int minMaxBreedBound, Vector2 deviation)
            {
                MinMax = minMax;
                MinMaxBreedBound = minMaxBreedBound;
                _minMaxDeviation = deviation.GetRandom();
            }
            

            public int Generate(bool minIsZero = true)
            {
                var from = MinMax.x - MinMaxBreedBound.x;
                if (minIsZero)
                    from = Mathf.Clamp(from, 0, Int32.MaxValue);

                return Mathf.FloorToInt(Mathf.Lerp(from, MinMax.y + MinMaxBreedBound.y, MinMaxDeviation)) + AbsAdd;
            }
        }
    }
}
