using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UnityFoundation.UI.Menus.MultiplayerLobbyMenu
{
    public class LobbyWaittingPanel : AbstractLobbyMenu
    {
        private readonly List<TMP_Text> playersText = new List<TMP_Text>();

        private Button startGameButton;

        private void Awake()
        {
            foreach(Transform wattingPlayerPanel in transform.Find("waitting_players"))
                playersText.Add(
                    wattingPlayerPanel.Find("text").GetComponent<TMP_Text>()
                );

            startGameButton = transform
                .Find("start_game_button")
                .GetComponent<Button>();
            startGameButton.onClick
                .AddListener(() => LobbyMenuManager.Instance.StartGame());

            transform
                .Find("leave_lobby_button")
                .GetComponent<Button>()
                .onClick
                .AddListener(() => LobbyMenuManager.Instance.LeaveLobby());
        }

        private void LobbyPlayersInfoHandler(List<string> players, bool isPartyOwner)
        {
            for(int i = 0; i < players.Count; i++)
                playersText[i].text = players[i];

            for(int i = players.Count; i < playersText.Count; i++)
                playersText[i].text = "Waitting player...";

            startGameButton.gameObject.SetActive(isPartyOwner);
            startGameButton.interactable = players.Count == 2;
        }

        public override void Show()
        {
            LobbyMenuManager.Instance.OnPartyPlayersInfoChanged 
                += LobbyPlayersInfoHandler;
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            LobbyMenuManager.Instance.OnPartyPlayersInfoChanged 
                -= LobbyPlayersInfoHandler;
            gameObject.SetActive(false);
        }
    }
}