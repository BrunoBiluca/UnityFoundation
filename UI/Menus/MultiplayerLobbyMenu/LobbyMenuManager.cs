using Assets.UnityFoundation.Code.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.UI.Menus.MultiplayerLobbyMenu
{
    public class LobbyMenuManager : Singleton<LobbyMenuManager>
    {
        [SerializeField] protected AbstractLobbyMenu landingPanel;
        [SerializeField] protected AbstractLobbyMenu enterAddressPanel;
        [SerializeField] protected AbstractLobbyMenu lobbyWattingPanel;

        public event Action OnClientConnected;
        public event Action OnClientDisconnected;

        public event Action<List<string>, bool> OnPartyPlayersInfoChanged;

        protected virtual void OnStart() { }
        protected virtual void OnHostLobby() { }
        protected virtual void OnJoinLobby(string address) { }
        protected virtual void OnLeaveLobby() { }
        protected virtual void OnStartGame() { }

        protected void InvokeClientConnected() => OnClientConnected?.Invoke();

        protected void InvokeClientDisconnected() => OnClientDisconnected?.Invoke();

        protected void InvokePlayerInfoUpdated(
            List<string> playersNames,
            bool isPartyOwner
        ) => OnPartyPlayersInfoChanged?.Invoke(playersNames, isPartyOwner);

        public void Start()
        {
            landingPanel.Show();
            enterAddressPanel.Hide();
            lobbyWattingPanel.Hide();

            OnStart();
        }

        public void HostLobby()
        {
            landingPanel.Hide();
            OnHostLobby();
        }

        public void JoinLobby(string address)
        {
            OnJoinLobby(address);
        }

        public void OpenLobbyAddressMenu()
        {
            landingPanel.Hide();
            enterAddressPanel.Show();
            lobbyWattingPanel.Hide();
        }

        public void LeaveLobby()
        {
            OnLeaveLobby();

            landingPanel.Show();
            enterAddressPanel.Hide();
            lobbyWattingPanel.Hide();
        }

        public void StartGame()
        {
            OnStartGame();
        }
    }
}