using System;
using AsepStudios.Mechanic.GameCore.Enum;
using AsepStudios.UI;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.Mechanic.GameCore
{
    public class Game : NetworkBehaviour
    {
        public static Game Instance { get; private set; }
        
        private readonly NetworkVariable<GameState> gameState = new();

        private readonly NetworkVariable<bool> isGameInitialized = new();
        private void Awake()
        {
            Instance = this;
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            gameState.OnValueChanged += GameStateOnValueChanged;
            ChangeViewByGameState();
        }
        
        public void ChangeGameState(GameState gameState)
        {
            if (!IsServer)
            {
                Debug.LogWarning("Clients can not change game state!");
            }

            switch (gameState)
            {
                case GameState.NotStarted:
                    TryResetGame();
                    break;
                case GameState.Playing:
                    TryInitializeGame();
                    break;
                case GameState.Paused:
                    break;
                case GameState.Over:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }

            this.gameState.Value = gameState;
        }

        private void TryInitializeGame()
        {
            if (isGameInitialized.Value) return;

            isGameInitialized.Value = true;
            
        }
        
        private void TryResetGame()
        {
            if (!isGameInitialized.Value) return;

            isGameInitialized.Value = false;
            
        }
        
        private void GameStateOnValueChanged(GameState previousGameState, GameState newGameState)
        {
            ChangeViewByGameState();
        }

        private void ChangeViewByGameState()
        {
            switch (gameState.Value)
            {
                case GameState.NotStarted:
                    ViewManager.ShowView<LobbyView>();
                    break;
                case GameState.Playing:
                    ViewManager.ShowView<GameView>();
                    break;
                case GameState.Paused:
                    ViewManager.ShowView<PauseView>();
                    break;
                case GameState.Over:
                    ViewManager.ShowView<GameOverView>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        
    }
}
