using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HSH
{
    [CreateAssetMenu(menuName = "HSH/Mods/DeviationModConfigAsset")]
    public class DeviationModConfigAsset : ModConfigAsset
    {
        [SerializeField] private StatType[] _targetStats;
        [SerializeField, Range(-1f, 1f)] private float _deviation;
        [SerializeField] private SmartChance _addModBaseChance = new SmartChance(0.2f);
        [SerializeField] private DeviationModConfigAsset _nextLevel;


        public override string GetDescription()
        {
            return string.Format(base.GetDescription(), $"{Mathf.FloorToInt(_deviation * 100)}%");
        }


        public override void TrySelect(BreedProcessor p)
        {
            var resultMod = p.ResultMods.FirstOrDefault(a => a.Info.GroupId == Info.GroupId && a.Info.Type == Info.Type);
            if (resultMod != null)
                return;

            if (_addModBaseChance.SampleNew())
                p.ResultMods.Add(this);
        }

        public override void Process(BreedProcessor p)
        {
            var seedMod = p.SeedBaseMods.OfType<DeviationModConfigAsset>().FirstOrDefault();
            var wombMod = p.WombBaseMods.OfType<DeviationModConfigAsset>().FirstOrDefault();

            if (seedMod != null && wombMod != null)
            {
                if (seedMod.Info.Level == wombMod.Info.Level && seedMod.Info.Type == wombMod.Info.Type && seedMod.Info.Level == Info.Level)
                {
                    if (_nextLevel != null)
                    {
                        p.ResultMods.Remove(this);
                        p.ResultMods.Add(_nextLevel);
                        return;
                    }
                }
                else if (seedMod.Info.Type != wombMod.Info.Type)
                {
                    p.ResultMods.Remove(this);
                }
            }
        }

        public override void Apply(BreedProcessor processor)
        {
            foreach (var st in processor.AllStatProcessors)
                st.Value.MinMaxDeviation += _deviation;

        }
    }
}
