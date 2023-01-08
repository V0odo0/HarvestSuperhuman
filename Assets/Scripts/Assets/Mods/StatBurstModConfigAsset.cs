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
            if (processor.ResultMods.Any(m => m.Info.Id == Info.Id))
                return;

            if (_addModBaseChance.SampleNew())
                processor.ResultMods.Add(this);
        }

        public override void Process(BreedProcessor processor)
        {

        }

        public override void Apply(BreedProcessor processor)
        {
            foreach (var st in processor.AllStatProcessors)
            {
                if (_targetStats.Contains(st.Key))
                    st.Value.AbsAdd += _statsAdd;
            }

            processor.ResultMods.Remove(this);
        }
    }
}
