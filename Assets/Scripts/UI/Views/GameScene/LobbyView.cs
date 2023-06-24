using AsepStudios.Utils;
using AsepStudios.Mechanic.LobbyCore;
using System;
using AsepStudios.Mechanic.GameCore;
using UnityEngine;
using UnityEngine.UI;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;
using TMPro;
using Unity.Netcode;

namespace AsepStudios.UI
{
    public class LobbyView : View
    {
        [SerializeField] private TextMeshProUGUI lobbyNameText;
        [SerializeField] private TextMeshProUGUI youAreTheHostText;

        [SerializeField] private Transform lobbyPlayerRectsTransform;
        [SerializeField] private LobbyPlayerRect lobbyPlayerRectPrefab;

        [SerializeField] private Button backButton;
        [SerializeField] private ButtonBase readyButton;
        [SerializeField] private Button startButton;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            Lobby.Instance.OnPlayerListChanged += Lobby_OnPlayerListChanged;
            Lobby.Instance.OnLobbyNameChanged += Lobby_OnLobbyNameChanged;
            LocalPlayer.Instance.OnPlayerAttached += LocalPlayer_OnPlayerAttached;
            
            if (Lobby.Instance.IsHostPlayerActive)
            {
                startButton.gameObject.SetActive(true);
                youAreTheHostText.gameObject.SetActive(true);
                startButton.onClick.AddListener(() =>
                {
                    if (Lobby.Instance.IsAllReady && Lobby.Instance.PlayerCount >= 0)
                    {
                        Game.Instance.StartGame();
                    }
                });
            }
            
            backButton.onClick.AddListener(ConnectionService.Disconnect);

            readyButton.OnClick.AddListener(() =>
            {
                LocalPlayer.Instance.Player.SetReadyLocal(!LocalPlayer.Instance.Player.GetReady());
            });
            
            RefreshPlayerList();
            RefreshLobbyName();
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            
            Lobby.Instance.OnPlayerListChanged -= Lobby_OnPlayerListChanged;
            Lobby.Instance.OnLobbyNameChanged -= Lobby_OnLobbyNameChanged;
            LocalPlayer.Instance.OnPlayerAttached -= LocalPlayer_OnPlayerAttached;
            LocalPlayer.Instance.Player.OnAnyPlayerPropertyChanged -= Player_OnAnyPlayerPropertyChanged;
        }
        
        private void LocalPlayer_OnPlayerAttached(object sender, EventArgs e)
        {
            LocalPlayer.Instance.Player.OnAnyPlayerPropertyChanged += Player_OnAnyPlayerPropertyChanged;
        }
        
        private void Lobby_OnPlayerListChanged(object sender, EventArgs e) { RefreshPlayerList(); }
        
        private void Lobby_OnLobbyNameChanged(object sender, EventArgs e) { RefreshLobbyName(); }
        
        private void Player_OnAnyPlayerPropertyChanged(object sender, EventArgs e) { SetReadyButton(); }

        private void RefreshPlayerList()
        {
            DestroyService.ClearChildren(lobbyPlayerRectsTransform);

            foreach(var player in Lobby.Instance.GetPlayers())
            {
                Instantiate(lobbyPlayerRectPrefab, lobbyPlayerRectsTransform).SetLobbyPlayerRect(player);
            }
        }

        private void RefreshLobbyName() { lobbyNameText.text = Lobby.Instance.GetLobbyName(); }

        private void SetReadyButton()
        {
            readyButton.Text.text = LocalPlayer.Instance.Player.GetReady() ? "READY" : "NOT READY";
            readyButton.ButtonColor = LocalPlayer.Instance.Player.GetReady() ? ResourceProvider.Colors.Orange : ResourceProvider.Colors.Beach;
        }
    }
}
