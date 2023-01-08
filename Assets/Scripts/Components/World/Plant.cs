using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HSH
{
    public class Plant : MonoBehaviourBase
    {
        [SerializeField] private List<PlantStageConfig> _stageConfigs;

        [Header("Refs")]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _plantSprite;

        public void SetState(PlantSlotProcessor.SlotState state)
        {
            _animator.gameObject.SetActive(state == PlantSlotProcessor.SlotState.FullyGrown || state == PlantSlotProcessor.SlotState.Breeding);

        }

        public void SetGrowthStage(int stage)
        {
            float swingMul = 1f - ((float)stage / GameManager.Data.GameCore.MaxGrowthStages);

            _animator.SetFloat("SwingMultiplier", swingMul);

            var stageConfig = _stageConfigs.FirstOrDefault(c => _stageConfigs.IndexOf(c) == stage) ?? _stageConfigs.First();
            _plantSprite.sprite = stageConfig.PlantSprite;
        }

        [Serializable]
        public class PlantStageConfig
        {
            public Sprite PlantSprite;
        }
    }
}
