using AsepStudios.TableChump.Input;
using AsepStudios.TableChump.Mechanics.LobbyCore;
using AsepStudios.TableChump.UI.Component.Custom;
using UnityEngine;

namespace AsepStudios.TableChump.UI.Views.GameScene
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

        private void Lobby_OnPlayerListChanged()
        {
            playerList.RefreshPlayerList();
        }
        
        private void OnEscapePerformed()
        {
            quitPanel.ChangeState();
        }
    }
}