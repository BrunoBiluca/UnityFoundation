using TMPro;
using UnityEngine.UI;

namespace Assets.UnityFoundation.UI.Menus.MultiplayerLobbyMenu
{
    public class EnterAddressPanel : AbstractLobbyMenu
    {
        private TMP_InputField addressInput;

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Show()
        {
            addressInput.interactable = true;
            gameObject.SetActive(true);
        }

        private void Awake()
        {
            addressInput = transform
                .Find("address_panel")
                .Find("address_input_field")
                .GetComponent<TMP_InputField>();

            transform.Find("address_panel")
                .Find("join_lobby_button")
                .GetComponent<Button>()
                .onClick
                .AddListener(
                    () => {
                        LobbyMenuManager.Instance.JoinLobby(addressInput.text);
                        addressInput.interactable = false;
                    }
                );
        }
    }
}