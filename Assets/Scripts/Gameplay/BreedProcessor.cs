using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    public class BreedProcessor
    {
        public readonly GameProfileData.DnaItemData Seed;
        public readonly GameProfileData.DnaItemData Womb;


        private GameProfileData.DnaItemData _result;
        

        public BreedProcessor(GameProfileData.DnaItemData seed, GameProfileData.DnaItemData womb)
        {
            Seed = seed;
            Womb = womb;

            _result = new GameProfileData.DnaItemData();
            _result.Type = Random.Range(0, 1) == 0 ? DnaItemType.Seed : DnaItemType.Womb;

            ProcessStats();
        }


        void ProcessStats()
        {
            _result.Stats.Bea = GetResultStat(Mathf.Min(Seed.Stats.Bea, Womb.Stats.Bea),
                Mathf.Max(Seed.Stats.Bea, Womb.Stats.Bea), GameManager.Data.GameCore.DefaultStatThreshold);
            _result.Stats.Imm = GetResultStat(Mathf.Min(Seed.Stats.Imm, Womb.Stats.Imm),
                Mathf.Max(Seed.Stats.Imm, Womb.Stats.Imm), GameManager.Data.GameCore.DefaultStatThreshold);
            _result.Stats.Int = GetResultStat(Mathf.Min(Seed.Stats.Int, Womb.Stats.Int),
                Mathf.Max(Seed.Stats.Int, Womb.Stats.Int), GameManager.Data.GameCore.DefaultStatThreshold);
            _result.Stats.Str = GetResultStat(Mathf.Min(Seed.Stats.Str, Womb.Stats.Str),
                Mathf.Max(Seed.Stats.Str, Womb.Stats.Str), GameManager.Data.GameCore.DefaultStatThreshold);
            _result.Stats.Vit = GetResultStat(Mathf.Min(Seed.Stats.Vit, Womb.Stats.Vit),
                Mathf.Max(Seed.Stats.Vit, Womb.Stats.Vit), GameManager.Data.GameCore.DefaultStatThreshold);
        }

        int GetResultStat(int min, int max, int threshold)
        {
            return Random.Range(min - threshold, max + threshold);
        }


        public GameProfileData.DnaItemData GetBreedResult()
        {
            return _result;
        }
    }
}
