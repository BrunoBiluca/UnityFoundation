using TMPro;
using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D.DebugHelpers
{
    public class PlayerDebugUI : MonoBehaviour
    {
        Player character;
        TextMeshProUGUI stateText;
        TextMeshProUGUI onGroundText;

        void Start()
        {
            character = GetComponentInParent<Player>();
            stateText = transform.Find("state_text").GetComponent<TextMeshProUGUI>();
            onGroundText = transform.Find("on_ground_text").GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            stateText.text = character.CurrentState.GetType().ToString();
            onGroundText.text = character.IsOnGround.ToString();
        }
    }
}