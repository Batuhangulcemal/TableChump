using AsepStudios.Utils;
using AsepStudios.Mechanic.Lobby;
using System;
using UnityEngine;
using AsepStudios.Mechanic.PlayerCore;
using UnityEngine.UI;

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

            RefreshPlayerList();

            backButton.onClick.AddListener(() =>
            {
                ConnectionService.Disconnect();
            });

        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Lobby.Instance.OnPlayerListChanged -= Lobby_OnPlayerListChanged;
        }

        private void Lobby_OnPlayerListChanged(object sender, EventArgs e)
        {
            RefreshPlayerList();
        }

        private void RefreshPlayerList()
        {
            DestroyService.ClearChildren(lobbyPlayerRectsTransform);

            foreach(Player player in Lobby.Instance.GetPlayers())
            {
                Instantiate(lobbyPlayerRectPrefab, lobbyPlayerRectsTransform);
            }

        }

    }
}
