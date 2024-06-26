﻿using AsepStudios.TableChump.Mechanics.GameCore.Enum;

namespace AsepStudios.TableChump.Mechanics.GameCore.Controller
{
    //this class is server only
    public class GameController
    {
        private RoundController roundController;

        private void Round_OnRoundEnded()
        {
            if (CheckIsGameShouldOver())
            {
                StopGame();
            }
            else
            {
                roundController.StartRound();
            }
        }

        private bool CheckIsGameShouldOver()
        {
            if (PlayerController.IsEveryoneAboveZero)
            {
                return false;
            }

            return true;
        }
        
        private void InitializeGame()
        {
            PlayerController.SetPlayersPoint(Game.Instance.PlayerPointStartValue);
            roundController = new RoundController();
            
            roundController.OnRoundEnded -= Round_OnRoundEnded;
            roundController.OnRoundEnded += Round_OnRoundEnded;
            
            roundController.StartRound();
            ChangeGameState(GameState.Playing);

        }

        public void StartGame()
        {
            if (Game.Instance == null) return;
            
            if (Game.Instance.IsArgsInitialized)
            {
                InitializeGame();
            }

        }
        
        public void StopGame()
        {
            ChangeGameState(GameState.Over);
        }

        public void RestartGame()
        {
            ChangeGameState(GameState.NotStarted);

        }
        
        private void ChangeGameState(GameState gameState)
        {
            Game.Instance.ChangeGameState(gameState);
        }
    }
}