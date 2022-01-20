using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D.DebugHelpers
{
    public class PlayerDebugUI : MonoBehaviour
    {
        [SerializeField] private bool showOnlyClassName;

        BaseCharacter2D character;
        TextMeshProUGUI stateText;
        TextMeshProUGUI onGroundText;

        void Start()
        {
            character = GetComponentInParent<BaseCharacter2D>();
            stateText = transform.Find("state_text")
                .GetComponent<TextMeshProUGUI>();

            var onGroundTextTransform = transform.Find("on_ground_text");
            if(character is Player)
            {
                onGroundText = transform
                    .Find("on_ground_text")
                    .GetComponent<TextMeshProUGUI>();
            }
            else
            {
                onGroundTextTransform.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            var state = character.CurrentState.GetType().ToString();

            if(showOnlyClassName)
                state = state.Split('.').Last();

            stateText.text = state;

            if(character is Player player)
            {
                onGroundText.text = player.IsOnGround.ToString();
            }
        }
    }
}