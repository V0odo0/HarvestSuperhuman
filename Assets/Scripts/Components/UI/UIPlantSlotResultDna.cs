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

        private PlantSlotProcessor _processor;
        private GameProfileData.DnaItemData _result;


        void ResetAllResults()
        {
            _dnaTypeObj.SetActive(false);

            _vitObj.SetActive(false);
            _strObj.SetActive(false);
            _intObj.SetActive(false);

            foreach (var m in _mods)
                m.gameObject.SetActive(false);
        }

        IEnumerator ShowResultDnaYield()
        {
            float delaySec = GameManager.Data.GameCore.GrowthStageTime;

            yield return new WaitForSeconds(delaySec);

            _dnaTypeImage.sprite = _result.Type == DnaItemType.Seed
                ? GameManager.Data.Sprites.SeedIcon
                : GameManager.Data.Sprites.WombIcon;
            _dnaTypeObj.SetActive(true);

            yield return new WaitForSeconds(delaySec);

            var statDiff = _result.Stats.Vit - _processor.BreedProcessor.VitProcessor.Avg;
            _vitText.text = $"{_result.Stats.Vit} [{(statDiff > 0 ? "+" : string.Empty)}{statDiff}]";
            _vitObj.SetActive(true);

            yield return new WaitForSeconds(delaySec);

            statDiff = _result.Stats.Str - _processor.BreedProcessor.StrProcessor.Avg;
            _strText.text = $"{_result.Stats.Str} [{(statDiff > 0 ? "+" : string.Empty)}{statDiff}]";
            _strObj.SetActive(true);

            yield return new WaitForSeconds(delaySec);

            statDiff = _result.Stats.Int - _processor.BreedProcessor.IntProcessor.Avg;
            _intText.text = $"{_result.Stats.Int} [{(statDiff > 0 ? "+" : string.Empty)}{statDiff}]";
            _intObj.SetActive(true);
            
            yield return new WaitForSeconds(delaySec);

            for (int i = 0; i < Mathf.Min(_result.Mods.Count, _mods.Length); i++)
            {
                _mods[i].Set(GameManager.Data.Mods.GetById(_result.Mods[i].Id));
                _mods[i].gameObject.SetActive(true);

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
