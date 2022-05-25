using UnityFoundation.Code;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.UI.Menus.MultiplayerLobbyMenu
{
    public class LobbyMenuManager : Singleton<LobbyMenuManager>
    {
        [SerializeField] protected LandingPanel landingPanel;
        [SerializeField] protected LobbyWaittingPanel lobbyWattingPanel;

        public event Action<List<string>, bool> OnPartyPlayersInfoChanged;

        protected virtual void OnStart() { }
        protected virtual void OnHostLobby() { }
        protected virtual void OnHostLobby(string roomName) { }
        protected virtual void OnJoinLobby(string address) { }
        protected virtual void OnLeaveLobby() { }
        protected virtual void OnStartGame() { }
        protected virtual void OnSetPlayerName(string playerName) { }

        protected void InvokePlayerInfoUpdated(
            List<string> playersNames,
            bool isPartyOwner
        ) => OnPartyPlayersInfoChanged?.Invoke(playersNames, isPartyOwner);

        public void Start()
        {
            landingPanel.Show();
            lobbyWattingPanel.Hide();

            OnStart();
        }

        public void HostLobby() => OnHostLobby();

        public void HostLobby(string roomName) => OnHostLobby(roomName);

        public void HostLobby(string address, string room) => OnHostLobby(room);

        public void JoinLobby(string address) => OnJoinLobby(address);

        public void SetPlayerName(string newPlayerName)
            => OnSetPlayerName(newPlayerName);

        public void LeaveLobby() => OnLeaveLobby();

        public void StartGame() => OnStartGame();

        public void OpenLandingPanel()
        {
            landingPanel.Show();
            lobbyWattingPanel.Hide();
        }

        public void OpenLobbyWattingRoom()
        {
            landingPanel.Hide();
            lobbyWattingPanel.Show();
        }
    }
}