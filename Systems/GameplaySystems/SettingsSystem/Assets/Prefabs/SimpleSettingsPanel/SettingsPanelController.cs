using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    [SerializeField] private bool panelStartClosed = true;

    private GameObject settingsPanel;
    private Animator settingsPanelAnimator;
    private Button settingsBtn;

    private void Awake()
    {
        settingsPanel = transform.Find("settings_panel").gameObject;
        settingsPanelAnimator = settingsPanel.GetComponent<Animator>();
        settingsPanel.transform.Find("back_btn")
            .GetComponent<Button>()
            .onClick
            .AddListener(() => CloseSettingsPanel());

        settingsBtn = transform.Find("settings_btn").GetComponent<Button>();
        settingsBtn.onClick.AddListener(() => OpenSettingsPanel());

        if(panelStartClosed)
            settingsPanel.SetActive(false);
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        settingsPanelAnimator.Play("slide_in");
        settingsBtn.gameObject.SetActive(false);
    }

    public void CloseSettingsPanel()
    {
        StartCoroutine(CloseSettings());
    }

    IEnumerator CloseSettings()
    {
        settingsPanelAnimator.Play("slide_out");
        yield return new WaitForSeconds(1f);
        settingsPanel.SetActive(false);
        settingsBtn.gameObject.SetActive(true);
    }
}
