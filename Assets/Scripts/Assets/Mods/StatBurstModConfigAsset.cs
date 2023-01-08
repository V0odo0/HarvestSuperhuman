using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HSH
{
    [CreateAssetMenu(menuName = "HSH/Mod/StatBurstModConfigAsset")]
    public class StatBurstModConfigAsset : ModConfigAsset
    {
        [SerializeField] private StatType[] _targetStats;
        [SerializeField] private SmartChance _addModBaseChance = new SmartChance(0.2f);
        [SerializeField] private int _statsAdd = 10;


        public override string GetDescription()
        {
            return string.Format(base.GetDescription(), _statsAdd.ToStringNice());
        }

        public override void TrySelect(BreedProcessor processor)
        {
            if (processor.ResultMods.Any(m => m.Info.GroupId == Info.GroupId))
                return;

            if (_addModBaseChance.SampleNew())
                processor.ResultMods.Add(this);
        }

        public override void Process(BreedProcessor processor)
        {

        }

        public override void Apply(BreedProcessor p)
        {
            var seedMod = p.SeedBaseMods.OfType<StatBurstModConfigAsset>().FirstOrDefault(m => m.Info.GroupId == Info.GroupId);
            var wombMod = p.WombBaseMods.OfType<StatBurstModConfigAsset>().FirstOrDefault(m => m.Info.GroupId == Info.GroupId);

            if (seedMod != null && wombMod != null)
            {
                foreach (var s in p.AllStatProcessors)
                {
                    if (seedMod._targetStats.Contains(s.Key))
                        s.Value.AbsAdd += seedMod._statsAdd;

                    if (wombMod._targetStats.Contains(s.Key))
                        s.Value.AbsAdd += wombMod._statsAdd;
                }

                p.ResultMods.Remove(this);
            }
        }
    }
}
