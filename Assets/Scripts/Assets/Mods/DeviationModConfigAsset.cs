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


        public override void Process(BreedProcessor processor)
        {
            foreach (var stat in processor.AllStatProcessors)
            {
                if (!_targetStats.Contains(stat.Key))
                    continue;

                stat.Value.MinMaxDeviation += _deviation;
            }
        }
    }
}
