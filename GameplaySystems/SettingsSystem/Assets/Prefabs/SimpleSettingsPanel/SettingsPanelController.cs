using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityFoundation.Code.Extensions;

namespace UnityFoundation.SettingsSystem
{
    public class SettingsPanelController : MonoBehaviour
    {
        [SerializeField] private bool panelStartClosed = true;
        [SerializeField] private Button settingsBtn;
        [SerializeField] private bool hideOnOpen = false;
        [SerializeField] private float animationSpeed = 1f;

        private GameObject settingsPanel;
        private Animator settingsPanelAnimator;

        private bool isOpened;

        private void Awake()
        {
            settingsPanel = transform.Find("settings_panel").gameObject;
            settingsPanelAnimator = settingsPanel.GetComponent<Animator>();
            settingsPanelAnimator.speed = animationSpeed;

            settingsPanel.transform.Find("back_btn")
                .GetComponent<Button>()
                .onClick
                .AddListener(() => CloseSettingsPanel());

            if(settingsBtn == null)
                settingsBtn = transform.Find("settings_btn").GetComponent<Button>();
            settingsBtn.onClick.AddListener(() => OpenSettingsPanel());

            isOpened = panelStartClosed;
            if(panelStartClosed)
                settingsPanel.SetActive(false);
        }

        public void OpenSettingsPanel()
        {
            StopCoroutine(nameof(CloseSettings));
            settingsPanel.SetActive(true);
            settingsPanelAnimator.Play("slide_in");

            if(hideOnOpen)
                settingsBtn.gameObject.SetActive(false);
        }

        public void CloseSettingsPanel()
        {
            StartCoroutine(nameof(CloseSettings));
        }

        IEnumerator CloseSettings()
        {
            settingsPanelAnimator.Play("slide_out");
            yield return new WaitForSeconds(1f);

            settingsPanel.SetActive(false);

            if(hideOnOpen)
                settingsBtn.gameObject.SetActive(true);
        }
    }
}