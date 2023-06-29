using System;
using AsepStudios.Mechanic.GameCore.Enum;

namespace AsepStudios.Mechanic.GameCore
{
    //this class is server only
    public class GameController
    {
        private RoundController roundController;
        private Game game;

        public GameController()
        {
            game = Game.Instance;
            
        }

        private void Round_OnRoundEnded(object sender, EventArgs e)
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
            return false;
        }
        
        private void InitializeGame()
        {
            
            roundController = new RoundController();
            
            roundController.OnRoundEnded -= Round_OnRoundEnded;
            roundController.OnRoundEnded += Round_OnRoundEnded;
            
            roundController.StartRound();

        }

        public void StartGame()
        {
            InitializeGame();
            ChangeGameState(GameState.Playing);

        }

        public void PauseGame()
        {
            ChangeGameState(GameState.Paused);
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