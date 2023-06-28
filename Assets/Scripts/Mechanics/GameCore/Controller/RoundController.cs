using System;
using System.Collections.Generic;

namespace AsepStudios.Mechanic.GameCore
{
    //this class is server only
    public class RoundController
    {
        public event EventHandler OnRoundEnded;
        
        private readonly Round round;
        private readonly BoardController boardController;
        private readonly PlayerController playerController;
        public RoundController()
        {
            round = Round.Instance;
            boardController = new BoardController();
            playerController = new PlayerController();
            
            playerController.OnAnyPlayerChosenCardChanged += PlayerController_OnOnAnyPlayerChosenCardChanged;
        }
        
        public void StartRound()
        {
            if (CheckIfNeedToDealCards())
            {
                CardDealer.DealCards(boardController);
            }
        }
        
        private bool CheckIfNeedToDealCards()
        {
            if (boardController.IsBoardEmpty())
            {
                return true;
            }

            return false;
        }

        private void PlayerController_OnOnAnyPlayerChosenCardChanged(object sender, EventArgs e)
        {
            if (playerController.IsEveryoneChose)
            {
                OnEveryoneChose();

            }
        }
        
        private void OnEveryoneChose()
        {
            //Put cards to board with order
            int[][] cards = playerController.ChosenCards;
            playerController.RemoveChosenCardsFromPlayers();
            
            boardController.PutChosenCards(cards);

            if (boardController.IsThereAnyLesserCardThanBoard(cards, out int clientId))
            {
                //Let The Player Choose A row
            }
            else
            {
                //PutCards
                ExecutePutAction(cards);
            }
        }

        private void ExecutePutAction(int[][] cards)
        {
            PutCardsToBoard(cards, out Dictionary<int, int> playerDamages);
            ApplyDamageToPlayers(playerDamages);
            
        }

        private void PutCardsToBoard(int[][] cards, out Dictionary<int, int> playerDamages)
        {
            boardController.PutCards(cards, out Dictionary<int, int> _playerDamages);
            playerDamages = _playerDamages;
        }

        private void ApplyDamageToPlayers(Dictionary<int, int> playerDamages)
        {
            
        }
    }
}