using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HSH.UI
{
    public class UIPlantSlotResultDna : UIMonoBehaviour
    {
        [SerializeField] private GameObject _dnaTypeObj;
        [SerializeField] private Image _dnaTypeImage;
        [SerializeField] private UIModButton[] _mods;

        [SerializeField] private GameObject _vitObj;
        [SerializeField] private TextMeshProUGUI _vitText;
        [SerializeField] private GameObject _strObj;
        [SerializeField] private TextMeshProUGUI _strText;
        [SerializeField] private GameObject _intObj;
        [SerializeField] private TextMeshProUGUI _intText;

        [Space]
        [SerializeField] private AudioSource _resultPopAudioSource;

        private PlantSlotProcessor _processor;
        private GameProfileData.DnaItemData _result;


        protected override void Awake()
        {
            base.Awake();

            foreach (var m in _mods)
            {
                m.Button.onClick.AddListener(() =>
                {
                    if (m.Config != null)
                        UIManager<GameUIManager>.Instance.GetPopUpPanel<UIModInfoPopUpPanel>().Show(m.Config);
                });
            }
        }

        void ResetAllResults()
        {
            _dnaTypeObj.SetActive(false);

            _vitObj.SetActive(false);
            _strObj.SetActive(false);
            _intObj.SetActive(false);

            foreach (var m in _mods)
                m.gameObject.SetActive(false);

            _resultPopAudioSource.pitch = 1f;
        }

        IEnumerator ShowResultDnaYield()
        {
            float delaySec = GameManager.Data.GameCore.GrowthStageTime;

            _resultPopAudioSource.Play();
            _resultPopAudioSource.pitch += 0.05f;
            yield return new WaitForSeconds(delaySec);

            _dnaTypeImage.sprite = _result.Type == DnaItemType.Seed
                ? GameManager.Data.Sprites.SeedIcon
                : GameManager.Data.Sprites.WombIcon;
            _dnaTypeObj.SetActive(true);


            _resultPopAudioSource.Play();
            _resultPopAudioSource.pitch += 0.05f;
            yield return new WaitForSeconds(delaySec);

            var statDiff = _result.Stats.Vit - _processor.BreedProcessor.VitProcessor.Avg;
            _vitText.text = $"{_result.Stats.Vit} <size=75%>[{(statDiff > 0 ? "+" : string.Empty)}{statDiff}]</size>";
            _vitObj.SetActive(true);


            _resultPopAudioSource.Play();
            _resultPopAudioSource.pitch += 0.05f;
            yield return new WaitForSeconds(delaySec);

            statDiff = _result.Stats.Str - _processor.BreedProcessor.StrProcessor.Avg;
            _strText.text = $"{_result.Stats.Str} <size=75%>[{(statDiff > 0 ? "+" : string.Empty)}{statDiff}]</size>";
            _strObj.SetActive(true);


            _resultPopAudioSource.Play();
            _resultPopAudioSource.pitch += 0.05f;
            yield return new WaitForSeconds(delaySec);

            statDiff = _result.Stats.Int - _processor.BreedProcessor.IntProcessor.Avg;
            _intText.text = $"{_result.Stats.Int} <size=75%>[{(statDiff > 0 ? "+" : string.Empty)}{statDiff}]</size>";
            _intObj.SetActive(true);


            _resultPopAudioSource.Play();
            _resultPopAudioSource.pitch += 0.05f;
            yield return new WaitForSeconds(delaySec);

            for (int i = 0; i < Mathf.Min(_result.Mods.Count, _mods.Length); i++)
            {
                _mods[i].Set(GameManager.Data.Mods.GetById(_result.Mods[i].Id));
                _mods[i].gameObject.SetActive(true);


                _resultPopAudioSource.Play();
                _resultPopAudioSource.pitch += 0.05f;
                yield return new WaitForSeconds(delaySec);
            }
        }

        public void Set(PlantSlotProcessor processor, bool animate = true)
        {
            ResetAllResults();

            _processor = processor;
            _result = processor.BreedProcessor.GetBreedResult();

            if (animate)
                StartCoroutine(nameof(ShowResultDnaYield));
        }
    }
}
