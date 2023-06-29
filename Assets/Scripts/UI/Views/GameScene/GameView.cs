using System;
using AsepStudios.Mechanic.GameCore;
using AsepStudios.Mechanic.LobbyCore;
using UnityEngine;
using UnityEngine.UI;
namespace AsepStudios.UI
{
    public class GameView : View
    {
        [SerializeField] private Button testButton;
        
        [SerializeField] private GameViewPlayerList playerList;
        [SerializeField] private GameViewDeck deck;
        [SerializeField] private GameViewBoard board;
        
        protected override void OnEnable()
        {
            base.OnEnable();

            Lobby.Instance.OnPlayerListChanged += Lobby_OnPlayerListChanged;
            playerList.RefreshPlayerList();
            deck.Initialize();
            board.Initialize();
            
            testButton.onClick.AddListener(() =>
            {
                ServerGameController.Controller.StopGame();
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