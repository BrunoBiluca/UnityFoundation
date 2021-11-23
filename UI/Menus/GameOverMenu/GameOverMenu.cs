using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.UnityFoundation.UI.Menus.GameOverMenu
{
    public class GameOverMenu : MonoBehaviour
    {
        private Transform menu;
        private TextMeshProUGUI winnerText;
        private Button actionButton;
        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }

        private void Awake()
        {
            menu = transform.Find("menu");

            winnerText = menu
                .Find("winner_text")
                .GetComponent<TextMeshProUGUI>();

            actionButton = menu
                .Find("action_button")
                .GetComponent<Button>();

            Hide();

            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        public virtual GameOverMenu Setup(
            string actionButtonName,
            UnityAction actionButtonBehaviour
        )
        {
            actionButton.transform
                .Find("text")
                .GetComponent<TextMeshProUGUI>()
                .text = actionButtonName;

            actionButton.onClick.AddListener(() => {
                Hide();
                actionButtonBehaviour();
            });

            return this;
        }

        public virtual void Show(string displayText)
        {
            winnerText.text = displayText;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}