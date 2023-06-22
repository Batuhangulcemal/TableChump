using AsepStudios.Utils;
using AsepStudios.Mechanic.LobbyCore;
using System;
using AsepStudios.Mechanic.GameCore;
using UnityEngine;
using UnityEngine.UI;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;

namespace AsepStudios.UI
{
    public class LobbyView : View
    {
        [SerializeField] private Transform lobbyPlayerRectsTransform;
        [SerializeField] private LobbyPlayerRect lobbyPlayerRectPrefab;

        [SerializeField] private Button backButton;
        [SerializeField] private Button readyButton;
        [SerializeField] private Button startButton;
        
        protected override void OnEnable()
        {
            base.OnEnable();

            Lobby.Instance.OnPlayerListChanged += Lobby_OnPlayerListChanged;
            LocalPlayer.Instance.OnPlayerAttached += LocalPlayer_OnPlayerAttached;
            
            if (Lobby.Instance.IsHostPlayerActive)
            {
                startButton.gameObject.SetActive(true);

                startButton.onClick.AddListener(() =>
                {
                    if (Lobby.Instance.IsAllReady)
                    {
                        Game.Instance.StartGame();
                    }
                });
            }

            backButton.onClick.AddListener(ConnectionService.Disconnect);

            readyButton.onClick.AddListener(() =>
            {
                LocalPlayer.Instance.Player.SetReady(!LocalPlayer.Instance.Player.GetReady());
            });
            
            RefreshPlayerList();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            Lobby.Instance.OnPlayerListChanged -= Lobby_OnPlayerListChanged;
            LocalPlayer.Instance.OnPlayerAttached -= LocalPlayer_OnPlayerAttached;
            LocalPlayer.Instance.Player.OnAnyPlayerPropertyChanged -= Player_OnAnyPlayerPropertyChanged;
        }

        private void Lobby_OnPlayerListChanged(object sender, EventArgs e)
        {
            RefreshPlayerList();
        }

        private void LocalPlayer_OnPlayerAttached(object sender, EventArgs e)
        {
            LocalPlayer.Instance.Player.OnAnyPlayerPropertyChanged += Player_OnAnyPlayerPropertyChanged;
            SetReadyButton();
        }

        private void Player_OnAnyPlayerPropertyChanged(object sender, EventArgs e)
        {
            SetReadyButton();
        }

        private void RefreshPlayerList()
        {
            DestroyService.ClearChildren(lobbyPlayerRectsTransform);

            foreach(var player in Lobby.Instance.GetPlayers())
            {
                Instantiate(lobbyPlayerRectPrefab, lobbyPlayerRectsTransform).SetLobbyPlayerRect(player);
            }
        }

        private void SetReadyButton()
        {
            if (readyButton == null) return;
            readyButton.name = LocalPlayer.Instance.Player.GetReady() ? "ready" : "notReady";
        }
    }
}
