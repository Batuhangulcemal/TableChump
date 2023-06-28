using System;
using System.Collections.Generic;
using AsepStudios.Mechanic.LobbyCore;
using AsepStudios.Utils;

namespace AsepStudios.Mechanic.GameCore
{
    //this class is server only
    public class RoundController
    {
        public event EventHandler OnRoundEnded;

        private readonly BoardController boardController;
        private readonly PlayerController playerController;
        public RoundController()
        {
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
            
            boardController.PutChosenCards(cards);

            if (boardController.IsThereAnyLesserCardThanBoard(cards, out int clientId))
            {
                //Let The Player Choose A row
            }
            else
            {
                //PutCards
                boardController.PutCards(cards, out Dictionary<int, int> dict);
            }
            
            //Remove chosen cards from players
            playerController.RemoveChosenCardsFromPlayers();

        }
    }
}