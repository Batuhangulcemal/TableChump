using System;
using AsepStudios.Mechanic.GameCore;
using AsepStudios.Mechanic.LobbyCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class GameView : View
    {
        [SerializeField] private Button testButton;

        [SerializeField] private GameViewPlayerList playerList;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            Lobby.Instance.OnPlayerListChanged += Lobby_OnPlayerListChanged;
            playerList.RefreshPlayerList();
            
            testButton.onClick.AddListener(() =>
            {
                Game.Instance.StopGame();
            });
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            Lobby.Instance.OnPlayerListChanged -= Lobby_OnPlayerListChanged;
            
        }

        private void Lobby_OnPlayerListChanged(object sender, EventArgs e)
        {
            playerList.RefreshPlayerList();
        }
    }
}