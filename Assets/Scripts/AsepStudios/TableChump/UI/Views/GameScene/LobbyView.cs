using System;
using AsepStudios.TableChump.Mechanics.GameCore.Controller;
using AsepStudios.TableChump.Mechanics.LobbyCore;
using AsepStudios.TableChump.Mechanics.PlayerCore.LocalPlayerCore;
using AsepStudios.TableChump.UI.Component.Custom;
using AsepStudios.TableChump.UI.Component.General;
using AsepStudios.TableChump.Utils.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.TableChump.UI.Views.GameScene
{
    public class LobbyView : View
    {
        [SerializeField] private TextMeshProUGUI lobbyNameText;
        [SerializeField] private TextMeshProUGUI youAreTheHostText;

        [SerializeField] private Transform lobbyPlayerRectsTransform;
        [SerializeField] private LobbyPlayerRect lobbyPlayerRectPrefab;

        [SerializeField] private Button logoutButton;
        [SerializeField] private ButtonBase readyButton;
        [SerializeField] private Button startButton;
        
        [SerializeField] private GameViewQuitPanel quitPanel;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            quitPanel.Initialize();
            
            Lobby.Instance.OnPlayerListChanged += Lobby_OnPlayerListChanged;
            Lobby.Instance.OnLobbyNameChanged += Lobby_OnLobbyNameChanged;
            LocalPlayer.Instance.Player.OnAnyPlayerPropertyChanged += Player_OnAnyPlayerPropertyChanged;

            
            if (Lobby.Instance.IsHostPlayerActive)
            {
                startButton.gameObject.SetActive(true);
                youAreTheHostText.gameObject.SetActive(true);
                startButton.onClick.AddListener(() =>
                {
                    if (Lobby.Instance.IsAllReady && Lobby.Instance.PlayerCount >= 0)
                    {
                        ServerGameController.Controller.StartGame();
                    }
                });
            }
            
            logoutButton.onClick.AddListener(() =>
            {
                quitPanel.ChangeState();
            });

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
            LocalPlayer.Instance.Player.OnAnyPlayerPropertyChanged -= Player_OnAnyPlayerPropertyChanged;
        }
        
        private void Lobby_OnPlayerListChanged(object sender, EventArgs e) { RefreshPlayerList(); }
        
        private void Lobby_OnLobbyNameChanged(object sender, EventArgs e) { RefreshLobbyName(); }
        
        private void Player_OnAnyPlayerPropertyChanged(object sender, EventArgs e) { SetReadyButton(); }

        private void RefreshPlayerList()
        {
            DestroyService.ClearChildren(lobbyPlayerRectsTransform);

            foreach(var player in Lobby.Instance.Players)
            {
                Instantiate(lobbyPlayerRectPrefab, lobbyPlayerRectsTransform).SetLobbyPlayerRect(player);
            }
        }

        private void RefreshLobbyName() { lobbyNameText.text = Lobby.Instance.GetLobbyName(); }

        private void SetReadyButton()
        {
            readyButton.Text.text = LocalPlayer.Instance.Player.GetReady() ? "READY" : "NOT READY";
            readyButton.ButtonColor = LocalPlayer.Instance.Player.GetReady() ? ColorService.Orange : ColorService.Beach;
        }
    }
}
