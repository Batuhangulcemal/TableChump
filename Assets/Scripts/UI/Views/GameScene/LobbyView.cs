using AsepStudios.Utils;
using AsepStudios.Mechanic.Lobby;
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
                startButton.onClick.AddListener(() =>
                {
                    if (Lobby.Instance.IsAllReady)
                    {
                        //StartGame();
                        Debug.Log("StartGame");

                    }
                    else
                    {
                        Debug.Log("Someones is not ready");
                    }
                });


            }
            else
            {
                startButton.gameObject.SetActive(false);
            }

            backButton.onClick.AddListener(() =>
            {
                ConnectionService.Disconnect();
            });

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
            readyButton.name = LocalPlayer.Instance.Player.GetReady() ? "ready" : "notReady";
        }

    }
}
