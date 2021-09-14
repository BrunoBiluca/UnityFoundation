using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    private TextMeshProUGUI winnerText;
    private Button actionButton;

    private void Awake()
    {
        winnerText = transform
            .Find("menu")
            .Find("winner_text")
            .GetComponent<TextMeshProUGUI>();

        actionButton = transform
            .Find("menu")
            .Find("action_button")
            .GetComponent<Button>();

        Hide();
    }

    public GameOverMenu Setup(string actionButtonName, UnityAction actionButtonBehaviour)
    {
        actionButton.transform
            .Find("text")
            .GetComponent<TextMeshProUGUI>()
            .text = actionButtonName;

        actionButton.onClick.AddListener(() => {
            actionButtonBehaviour();
            Hide();
        });

        return this;
    }

    public void Show(string displayText)
    {
        winnerText.text = displayText;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
