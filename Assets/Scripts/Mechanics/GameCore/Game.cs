using System;
using AsepStudios.Mechanic.GameCore.Enum;
using AsepStudios.Mechanic.LobbyCore;
using AsepStudios.UI;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.Mechanic.GameCore
{
    public class Game : NetworkBehaviour
    {
        public event EventHandler OnGameStateChanged;
        public static Game Instance { get; private set; }

        [SerializeField] private Board board;
        public Board Board => board;

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
        
        public void StartGame(){ ChangeGameState(Enum.GameState.Playing); }
        public void PauseGame(){ ChangeGameState(Enum.GameState.Paused); }
        public void StopGame(){ ChangeGameState(Enum.GameState.Over); }
        public void RestartGame(){ ChangeGameState(Enum.GameState.NotStarted); }
        
        private void TryInitializeGame()
        {
            if (isGameInitialized.Value) return;

            board.Initialize();
            
            isGameInitialized.Value = true;
            Debug.Log("Deal Cards");
            
        }
        
        private void TryResetGame()
        {
            if (!isGameInitialized.Value) return;

            board.Reset();
            
            isGameInitialized.Value = false;
        }
        
        private void ChangeGameState(GameState gameState)
        {
            if (!IsServer)
            {
                Debug.LogWarning("Clients can not change game state!");
                return;
            }
            
            switch (gameState)
            {
                case Enum.GameState.NotStarted:
                    TryResetGame();
                    break;
                case Enum.GameState.Playing:
                    TryInitializeGame();
                    break;
                case Enum.GameState.Paused:
                    break;
                case Enum.GameState.Over:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
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
                    ViewManager.ShowView<LobbyView>();
                    TryResetGame();
                    break;
                case Enum.GameState.Playing:
                    ViewManager.ShowView<GameView>();
                    TryInitializeGame();
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
