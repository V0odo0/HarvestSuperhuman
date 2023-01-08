using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HSH.UI;
using UnityEngine;
using UnityEngine.UI;

namespace HSH
{
    public class PlantSlot : MonoBehaviourBase
    {
        public event EventHandler<PlantSlotProcessor.SlotState> StateActionRequested;

        public int Id => _id;
        [SerializeField] private int _id;

        public PlantSlotProcessor Processor { get; private set; }

        [SerializeField] private SlotStateConfig[] _slotStateConfigs;

        [Header("Refs")]
        [SerializeField] private GameObject _root;
        [SerializeField] private UIPlantSlotResultDna _slotResultDna;
        [SerializeField] private UIButton _actionButton;
        [SerializeField] private CanvasGroup _actionButtonCanvasGroup;

        [SerializeField] private Image _growthTimerFillImage;
        [SerializeField] private Plant _plant;
        [SerializeField] private ParticleSystem _startBreedingStateParticles;
        [SerializeField] private ParticleSystem _breedingStateParticles;

        [Space]
        [SerializeField] private Animator _actionButtonAnimator;
        [SerializeField] private AudioSource _plantAudioSource;
        [SerializeField] private AudioSource _breedingAudioSource;
        [SerializeField] private AudioSource _harvestAudioSource;


        protected override void Awake()
        {
            base.Awake();
            
            _actionButton.Button.onClick.AddListener(() =>
            {
                if (Processor != null)
                    StateActionRequested?.Invoke(this, Processor.State);
            });
        }
        
        protected override void Update()
        {
            base.Update();

            if (Processor != null)
            {
                if (Processor.State == PlantSlotProcessor.SlotState.Breeding)
                {
                    var growthTimeNorm = Processor.GrowthTime / (GameManager.Data.GameCore.MaxGrowthStages *
                                                                 GameManager.Data.GameCore.GrowthStageTime);
                    _growthTimerFillImage.fillAmount = growthTimeNorm;
                }
            }
        }


        void ProcessorOnPropertyChanged(string obj)
        {
            switch (obj)
            {
                case nameof(PlantSlotProcessor.GrowthStage):
                    _plant.SetGrowthStage(Processor.GrowthStage);
                    break;
                case nameof(PlantSlotProcessor.State):
                    UpdateState(Processor.State);
                    break;
            }
        }


        void UpdateState(PlantSlotProcessor.SlotState state)
        {
            _root.SetActive(state != PlantSlotProcessor.SlotState.None);
            if (state == PlantSlotProcessor.SlotState.None)
                return;

            _slotResultDna.gameObject.SetActive(state == PlantSlotProcessor.SlotState.Breeding || state == PlantSlotProcessor.SlotState.FullyGrown);
            
            switch (state)
            {
                case PlantSlotProcessor.SlotState.Locked:
                    _actionButton.Button.interactable = false;
                    break;
                case PlantSlotProcessor.SlotState.Empty:
                    _actionButton.Button.interactable = true;

                    _harvestAudioSource.Play();
                    break;
                case PlantSlotProcessor.SlotState.Breeding:
                    _plantAudioSource.Play();
                    _breedingAudioSource.Play();

                    _startBreedingStateParticles.Play();
                    _actionButton.Button.interactable = false;
                    _slotResultDna.Set(Processor);
                    break;
                case PlantSlotProcessor.SlotState.FullyGrown:
                    _actionButton.Button.interactable = true;
                    _growthTimerFillImage.fillAmount = 1f;

                    break;
            }

            var slotStateConfig = _slotStateConfigs.FirstOrDefault(p => p.SourceState == state) ??
                                  _slotStateConfigs.First();
            _actionButton.Icon = slotStateConfig.ActionButtonIcon;

            _growthTimerFillImage.enabled = state == PlantSlotProcessor.SlotState.Breeding || state == PlantSlotProcessor.SlotState.FullyGrown;
            _plant.SetState(Processor.State);
            
            _actionButtonAnimator.SetInteger("State", (int) Processor.State);
            _actionButtonAnimator.SetTrigger("StateChanged");
            _actionButtonCanvasGroup.alpha = _actionButton.Button.interactable ? 1f : 0.5f;

            if (state == PlantSlotProcessor.SlotState.Breeding)
                _breedingStateParticles.Play();
            else _breedingStateParticles.Stop();

        }

        public void SetProcessor(PlantSlotProcessor processor)
        {
            if (processor == null)
            {
                Processor = null;
                UpdateState(PlantSlotProcessor.SlotState.None);
                return;
            }

            Processor = processor;
            Processor.PropertyChanged += ProcessorOnPropertyChanged;

            UpdateState(Processor.State);
        }


        [Serializable]
        public class SlotStateConfig
        {
            public PlantSlotProcessor.SlotState SourceState;
            public Sprite ActionButtonIcon;
        }
    }
}
