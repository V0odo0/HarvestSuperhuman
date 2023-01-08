using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HSH
{
    [Serializable]
    public class SmartChance
    {
        public float BaseChance
        {
            get => _baseChance;
            set => _baseChance = Mathf.Clamp01(value);
        }
        [SerializeField, Range(0f, 1f)] private float _baseChance;


        public SmartChance() { }

        public SmartChance(float baseChance)
        {
            _baseChance = baseChance;
        }


        public bool SampleNew()
        {
            return _baseChance >= Random.Range(0f, 1f);
        }
    }
}
