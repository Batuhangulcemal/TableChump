using AsepStudios.Utils;
using AsepStudios.Mechanic.LobbyCore;
using System;
using UnityEngine;
using AsepStudios.Mechanic.PlayerCore;
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

            RefreshPlayerList();

            if (Lobby.Instance.IsHostPlayerActive)
            {
                startButton.gameObject.SetActive(true);

                startButton.onClick.AddListener(() =>
                {
                    //StartGame();
                    Debug.Log(Lobby.Instance.IsAllReady ? "StartGame" : "Someones is not ready");
                });
            }

            backButton.onClick.AddListener(ConnectionService.Disconnect);

            readyButton.onClick.AddListener(() =>
            {
                LocalPlayer.Instance.Player.SetReady(!LocalPlayer.Instance.Player.GetReady());
            });


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

            foreach(Player player in Lobby.Instance.GetPlayers())
            {
                LobbyPlayerRect playerRect = Instantiate(lobbyPlayerRectPrefab, lobbyPlayerRectsTransform);
                playerRect.SetLobbyPlayerRect(player);
            }
        }

        private void SetReadyButton()
        {
            if (readyButton == null) return;
            readyButton.name = LocalPlayer.Instance.Player.GetReady() ? "ready" : "notReady";
        }

    }
}
