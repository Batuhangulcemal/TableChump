using System;
using AsepStudios.Input;
using AsepStudios.Mechanic.GameCore;
using AsepStudios.Mechanic.LobbyCore;
using UnityEngine;
using UnityEngine.UI;
namespace AsepStudios.UI
{
    public class GameView : View
    {
        [SerializeField] private GameViewPlayerList playerList;
        [SerializeField] private GameViewDeck deck;
        [SerializeField] private GameViewBoard board;
        [SerializeField] private GameViewQuitPanel quitPanel;
        
        protected override void OnEnable()
        {
            base.OnEnable();

            Lobby.Instance.OnPlayerListChanged += Lobby_OnPlayerListChanged;
            PlayerInput.Instance.OnEscapePerformed += OnEscapePerformed;

            playerList.RefreshPlayerList();
            deck.Initialize();
            board.Initialize();
            quitPanel.Initialize();
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            
            Lobby.Instance.OnPlayerListChanged -= Lobby_OnPlayerListChanged;
            PlayerInput.Instance.OnEscapePerformed -= OnEscapePerformed;
            
        }

        private void Lobby_OnPlayerListChanged(object sender, EventArgs e)
        {
            playerList.RefreshPlayerList();
        }
        
        private void OnEscapePerformed(object sender, EventArgs e)
        {
            quitPanel.ChangeState();
        }
    }
}