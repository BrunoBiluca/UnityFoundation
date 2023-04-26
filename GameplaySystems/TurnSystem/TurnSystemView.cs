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
        private VisibilityMono enemyTurnDisplay;

        public void Awake()
        {
            text = transform.FindComponent<TextMeshProUGUI>("turn_text");

            endTurnButton = transform
                .FindComponent<Button>("end_turn_button");
            endTurnButton.gameObject.AddComponent<VisibilityMono>().Show();

            endTurnButton.onClick.AddListener(EndPlayerTurn);

            enemyTurnDisplay = transform
                .FindComponent<VisibilityMono>("enemy_turn_display");
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

            endTurnButton.GetComponent<VisibilityMono>().Hide();
            enemyTurnDisplay.Show();
        }

        private void EndEnemyTurn()
        {
            endTurnButton.GetComponent<VisibilityMono>().Show();
            enemyTurnDisplay.Hide();
        }

        private void UpdateTurnView()
        {
            text.text = $"Turn: {turnSystem.CurrentTurn}";
        }
    }
}
