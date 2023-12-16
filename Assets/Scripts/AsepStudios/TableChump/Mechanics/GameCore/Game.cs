using System;
using AsepStudios.TableChump.Mechanics.GameCore.Enum;
using AsepStudios.TableChump.Mechanics.PlayerCore.LocalPlayerCore;
using AsepStudios.TableChump.UI;
using AsepStudios.TableChump.UI.Views.GameScene;
using Unity.Netcode;

namespace AsepStudios.TableChump.Mechanics.GameCore
{
    public class Game : NetworkBehaviour
    {
        public event EventHandler OnGameStateChanged;
        public static Game Instance { get; private set; }
        
        public readonly NetworkVariable<GameState> GameState = new();
        
        private readonly NetworkVariable<GameArgs> gameArgs = new(new GameArgs()
        {
            IsArgsInitialized = false
        });

        public bool IsArgsInitialized => gameArgs.Value.IsArgsInitialized;
        public int PlayerPointStartValue => gameArgs.Value.PlayerPointStartValue;
        public int MaxPlayerCount => gameArgs.Value.MaxPlayerCount;
        private void Awake()
        {
            Instance = this;
        }
        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsServer)
            {
                SetGameArgs(GameArgsHolder.GameArgs);
            }
            GameState.OnValueChanged += GameStateOnValueChanged;
            ChangeViewByGameState();
        }
        
        public void ChangeGameState(GameState gameState)
        {
            GameState.Value = gameState;
        }

        private void SetGameArgs(GameArgs args)
        {
            gameArgs.Value = args;
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
                case TableChump.Mechanics.GameCore.Enum.GameState.NotStarted:
                    if (LocalPlayer.Instance.Player != null)
                    {
                        ViewManager.ShowView<LobbyView>();
                    }
                    else
                    {
                        ViewManager.ShowView<WaitLocalPlayerView>();
                    }
                    break;
                case TableChump.Mechanics.GameCore.Enum.GameState.Playing:
                    ViewManager.ShowView<GameView>();
                    break;
                case TableChump.Mechanics.GameCore.Enum.GameState.Over:
                    ViewManager.ShowView<GameOverView>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        
    }
}
