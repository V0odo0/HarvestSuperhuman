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

        [SerializeField] private UIDnaStat _vitDnaStat;
        [SerializeField] private UIDnaStat _strDnaStat;
        [SerializeField] private UIDnaStat _intDnaStat;

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

            _vitDnaStat.gameObject.SetActive(false);
            _strDnaStat.gameObject.SetActive(false);
            _intDnaStat.gameObject.SetActive(false);

            foreach (var m in _mods)
                m.gameObject.SetActive(false);

            _resultPopAudioSource.pitch = 1f;
        }

        IEnumerator ShowResultDnaYield()
        {
            float delaySec = GameManager.Data.GameCore.GrowthStageTime;

            yield return new WaitForSeconds(delaySec);

            _dnaTypeImage.sprite = _result.Type == DnaItemType.Seed
                ? GameManager.Data.Sprites.SeedIcon
                : GameManager.Data.Sprites.WombIcon;
            _dnaTypeObj.SetActive(true);


            _resultPopAudioSource.Play();
            _resultPopAudioSource.pitch += 0.05f;
            yield return new WaitForSeconds(delaySec);

            _vitDnaStat.SetDiff(_processor.BreedProcessor.VitProcessor.Avg, _result.Stats.Vit, GameManager.Data.GameCore.MaxDnaStatValue, StatType.Vit);
            _vitDnaStat.gameObject.SetActive(true);


            _resultPopAudioSource.Play();
            _resultPopAudioSource.pitch += 0.05f;
            yield return new WaitForSeconds(delaySec);

            _strDnaStat.SetDiff(_processor.BreedProcessor.StrProcessor.Avg, _result.Stats.Str, GameManager.Data.GameCore.MaxDnaStatValue, StatType.Str);
            _strDnaStat.gameObject.SetActive(true);


            _resultPopAudioSource.Play();
            _resultPopAudioSource.pitch += 0.05f;
            yield return new WaitForSeconds(delaySec);
            
            _intDnaStat.SetDiff(_processor.BreedProcessor.IntProcessor.Avg, _result.Stats.Int, GameManager.Data.GameCore.MaxDnaStatValue, StatType.Int);
            _intDnaStat.gameObject.SetActive(true);


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
