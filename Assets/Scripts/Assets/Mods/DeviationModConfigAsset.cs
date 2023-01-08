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
        protected string DisableProcessTag => $"DISABLE_PROCESS_{Info.GroupId}";

        [SerializeField] private StatType[] _targetStats;
        [SerializeField, Range(-1f, 1f)] private float _deviation;
        [SerializeField] private SmartChance _addModBaseChance = new SmartChance(0.2f);


        public override string GetDescription()
        {
            return string.Format(base.GetDescription(), $"{Mathf.FloorToInt(_deviation * 100)}%");
        }

        public override void Process(BreedProcessor p)
        {
            if (p.Tags.Contains(DisableProcessTag))
                return;

            var seedMod = p.SeedBaseMods.OfType<DeviationModConfigAsset>().FirstOrDefault();
            var wombMod = p.WombBaseMods.OfType<DeviationModConfigAsset>().FirstOrDefault();

            if (seedMod != null && wombMod != null)
            {
                if (seedMod.Info.Type != wombMod.Info.Type)
                {
                    p.SeedBaseMods.Remove(seedMod);
                    p.WombBaseMods.Remove(wombMod);
                    p.Tags.Add(DisableProcessTag);
                    return;
                }

                if (seedMod.Info.Level != wombMod.Info.Level)
                {
                    p.SeedBaseMods.Remove(seedMod);
                    p.WombBaseMods.Remove(wombMod);
                    var tarMod = seedMod.Info.Level > wombMod.Info.Level ? seedMod : wombMod;
                    p.ResultMods.Add(tarMod);
                    p.Tags.Add(DisableProcessTag);
                    return;
                }

                if (seedMod.Info.Level == wombMod.Info.Level)
                {
                    var nextLevelMod = GameManager.Data.Mods.All.OfType<DeviationModConfigAsset>()
                        .FirstOrDefault(m => m.Info.Level == seedMod.Info.Level + 1);

                    if (nextLevelMod != null)
                    {
                        bool hasNextLevelMod = p.ResultMods.Any(m => m.Info.Id == nextLevelMod.Info.Id);
                        if (!hasNextLevelMod)
                        {
                            p.SeedBaseMods.Remove(seedMod);
                            p.WombBaseMods.Remove(wombMod);
                            p.ResultMods.Add(nextLevelMod);
                            p.Tags.Add(DisableProcessTag);
                            return;
                        }
                    }
                }
                
            }

            if (p.ResultMods.OfType<DeviationModConfigAsset>().Any())
                return;
            
            if (seedMod != null)
            {
                p.SeedBaseMods.Remove(seedMod);
                p.ResultMods.Add(seedMod);
                p.Tags.Add(DisableProcessTag);
                return;
            }

            if (wombMod != null)
            {
                p.WombBaseMods.Remove(wombMod);
                p.ResultMods.Add(wombMod);
                p.Tags.Add(DisableProcessTag);
                return;
            }

            if (Info.Level == 0 && _addModBaseChance.SampleNew())
            {
                p.ResultMods.Add(this);
                p.Tags.Add(DisableProcessTag);
            }
        }
    }
}
