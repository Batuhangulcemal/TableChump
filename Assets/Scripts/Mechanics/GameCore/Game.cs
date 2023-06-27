using System;
using AsepStudios.Mechanic.GameCore.Enum;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;
using AsepStudios.UI;
using Unity.Netcode;


namespace AsepStudios.Mechanic.GameCore
{
    public class Game : NetworkBehaviour
    {
        public event EventHandler OnGameStateChanged;
        public static Game Instance { get; private set; }
        
        public readonly NetworkVariable<GameState> GameState = new();

        private readonly NetworkVariable<bool> isGameInitialized = new();
        
        private void Awake()
        {
            Instance = this;
        }
        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            GameState.OnValueChanged += GameStateOnValueChanged;
            ChangeViewByGameState();
        }
        
        public void ChangeGameState(GameState gameState)
        {
            GameState.Value = gameState;
        }

        private void GameStateOnValueChanged(GameState previousGameState, GameState newGameState)
        {
            OnGameStateChanged?.Invoke(this, EventArgs.Empty);
            ChangeViewByGameState();
        }

        private void ChangeViewByGameState()
        {
            switch (GameState.Value)
            {
                case Enum.GameState.NotStarted:
                    if (LocalPlayer.Instance.Player != null)
                    {
                        ViewManager.ShowView<LobbyView>();
                    }
                    else
                    {
                        ViewManager.ShowView<WaitLocalPlayerView>();
                    }
                    break;
                case Enum.GameState.Playing:
                    ViewManager.ShowView<GameView>();
                    break;
                case Enum.GameState.Paused:
                    ViewManager.ShowView<PauseView>();
                    break;
                case Enum.GameState.Over:
                    ViewManager.ShowView<GameOverView>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        
    }
}
