using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityFoundation.UI
{
    public class GameOverMenu : MonoBehaviour
    {
        private Transform menu;
        private TextMeshProUGUI winnerText;
        private Button actionButton;
        private TextMeshProUGUI actionButtonText;

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }

        private void Awake()
        {
            menu = transform.Find("menu");

            winnerText = menu.Find("winner_text").GetComponent<TextMeshProUGUI>();

            var actionButtonGO = menu.Find("action_button");
            actionButton = actionButtonGO.GetComponent<Button>();
            actionButtonText = actionButtonGO.Find("text").GetComponent<TextMeshProUGUI>();
            actionButton.onClick.AddListener(Hide);

            Hide();

            OnAwake();
        }

        private void Start()
        {
            OnStart();

            SetupByAnnexedComponent();
        }

        public void SetupByAnnexedComponent()
        {
            if(!TryGetComponent(out IGameOverAction action))
                return;

            actionButtonText.text = action.Name;
            actionButton.onClick.AddListener(action.Execute);
        }

        public GameOverMenu Setup(
            string actionButtonName,
            UnityAction actionButtonBehaviour
        )
        {
            actionButtonText.text = actionButtonName;

            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(actionButtonBehaviour);

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