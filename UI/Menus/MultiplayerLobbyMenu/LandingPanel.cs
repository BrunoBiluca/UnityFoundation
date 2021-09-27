using UnityEngine.UI;

namespace Assets.UnityFoundation.UI.Menus.MultiplayerLobbyMenu
{
    public class LandingPanel : AbstractLobbyMenu
    {
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
            transform
                .Find("host_lobby_button")
                .GetComponent<Button>()
                .onClick
                .AddListener(
                    () => LobbyMenuManager.Instance.HostLobby()
                );

            transform
                .Find("join_lobby_button")
                .GetComponent<Button>()
                .onClick
                .AddListener(
                    () => LobbyMenuManager.Instance.OpenLobbyAddressMenu()
                );

        }
    }
}