using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UnityFoundation.UI.Menus.MultiplayerLobbyMenu
{
    public class LandingPanel : AbstractLobbyMenu
    {
        [SerializeField] private bool canSetPlayerName;
        [SerializeField] private bool canSetAddress;
        [SerializeField] private bool canSetRoomName;
        [SerializeField] private bool startButtonsDisabled;

        private Button hostLobbyButton;
        private Button joinLobbyButton;

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        private void Awake()
        {
            SetupButtons();
            SetupPlayerName();
            SetupRoomName();
            SetupAddress();
        }

        private void SetupButtons()
        {
            hostLobbyButton = transform.Find("host_lobby_button")
                .GetComponent<Button>();
            hostLobbyButton
                .onClick
                .AddListener(
                    () => LobbyMenuManager.Instance.HostLobby()
                );
            hostLobbyButton.interactable = !startButtonsDisabled;

            joinLobbyButton = transform.Find("join_lobby_button")
                .GetComponent<Button>();
            joinLobbyButton.interactable = !startButtonsDisabled;
        }

        private void SetupPlayerName()
        {
            var playerNameField = transform
                .Find("player_name_field")
                .GetComponent<TMP_InputField>();

            playerNameField
                .onValueChanged
                .AddListener(
                    (newValue) => LobbyMenuManager.Instance.SetPlayerName(newValue)
                );

            playerNameField.gameObject.SetActive(canSetPlayerName);

        }

        private void SetupRoomName()
        {
            var roomNameField = transform
                .Find("room_name_field")
                .GetComponent<TMP_InputField>();
            roomNameField.gameObject.SetActive(canSetRoomName);

            if(canSetRoomName)
            {
                hostLobbyButton.onClick.RemoveAllListeners();
                hostLobbyButton.onClick.AddListener(
                    () => LobbyMenuManager.Instance.HostLobby(roomNameField.text)
                );

                joinLobbyButton.onClick.RemoveAllListeners();
                joinLobbyButton.onClick.AddListener(
                    () => LobbyMenuManager.Instance.JoinLobby(roomNameField.text)
                );
            }

        }

        private void SetupAddress()
        {
            var addressField = transform
                .Find("address_field")
                .GetComponent<TMP_InputField>();
            addressField.gameObject.SetActive(canSetAddress);

            if(canSetAddress)
            {
                joinLobbyButton.onClick.RemoveAllListeners();
                joinLobbyButton.onClick.AddListener(
                    () => LobbyMenuManager.Instance.JoinLobby(addressField.text)
                );
            }

        }

        public void EnableButtons()
        {
            hostLobbyButton.interactable = true;
            joinLobbyButton.interactable = true;
        }
    }
}