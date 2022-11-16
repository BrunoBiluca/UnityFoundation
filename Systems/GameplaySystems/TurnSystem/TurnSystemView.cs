using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.TurnSystem
{
    public class TurnSystemView : MonoBehaviour
    {
        private ITurnSystem turnSystem;
        private TextMeshProUGUI text;
        private Button endTurnButton;
        private VisibilityHandlerMono enemyTurnDisplay;

        public void Awake()
        {
            text = transform.FindComponent<TextMeshProUGUI>("turn_text");

            endTurnButton = transform
                .FindComponent<Button>("end_turn_button");
            endTurnButton.gameObject.AddComponent<VisibilityHandlerMono>().Show();

            endTurnButton.onClick.AddListener(EndPlayerTurn);

            enemyTurnDisplay = transform
                .FindComponent<VisibilityHandlerMono>("enemy_turn_display");
        }

        public void Setup(ITurnSystem turnSystem)
        {
            this.turnSystem = turnSystem;

            turnSystem.OnPlayerTurnEnded += UpdateTurnView;
            UpdateTurnView();

            turnSystem.OnEnemyTurnEnded += EndEnemyTurn;
        }

        private void EndPlayerTurn()
        {
            turnSystem.EndPlayerTurn();

            endTurnButton.GetComponent<VisibilityHandlerMono>().Hide();
            enemyTurnDisplay.Show();
        }

        private void EndEnemyTurn()
        {
            endTurnButton.GetComponent<VisibilityHandlerMono>().Show();
            enemyTurnDisplay.Hide();
        }

        private void UpdateTurnView()
        {
            text.text = $"Turn: {turnSystem.CurrentTurn}";
        }
    }
}
