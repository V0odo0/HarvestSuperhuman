using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HSH.UI
{
    public class TitleUIManager : UIManager<TitleUIManager>
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _openHomeButton;
        [SerializeField] private TextMeshProUGUI _versionText;


        protected override void Awake()
        {
            base.Awake();

            _versionText.text = Application.version;

            _startButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
            });

            _openHomeButton.onClick.AddListener(() =>
            {
                Application.OpenURL("https://twitter.com/Voodoo2211");
            });
        }
    }
}
