using System;
using System.Collections.Generic;
using System.Linq;
using AsepStudios.Mechanic.GameCore.Enum;

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
            playerController.OnAnyPlayerChosenRowChanged += PlayerController_OnAnyPlayerChosenRowChanged;
        }
        
        public void StartRound()
        {
            if (CheckIfNeedToDealCards())
            {
                round.ChangeRoundState(RoundState.Dealing);
                CardDealer.DealCards(boardController);
            }
            
            round.ChangeRoundState(RoundState.WaitingForPlayers);
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
            if (playerController.IsEveryoneChoseCard)
            {
                OnEveryoneChose();

            }
        }
        
        private void PlayerController_OnAnyPlayerChosenRowChanged(object sender, EventArgs e)
        {
            if (round.RoundState != RoundState.WaitingForPlayerChooseARow) return;

            if (!playerController.IsRightPlayerChoseRow) return;
            
            //right player chose a row.
            
            ExecuteRowTakeAction();
        }
        
        private void OnEveryoneChose()
        {
            if (round.RoundState != RoundState.WaitingForPlayers) return;
            //Put cards to board with order
            int[][] cards = playerController.ChosenCards;
            
            boardController.PutChosenCards(cards);

            if (boardController.IsThereAnyLesserCardThanBoard(cards, out int playerId))
            {
                //Let The Player Choose A row
                round.ChangeRowChoosePlayer(playerId);
                round.ChangeRoundState(RoundState.WaitingForPlayerChooseARow);
            }
            else
            {
                //PutCards
                ExecutePutAction(cards);
            }
        }

        private void ExecutePutAction(int[][] cards)
        {
            round.ChangeRoundState(RoundState.Animating);
            playerController.RemoveChosenCardsFromPlayers();
            playerController.ResetChosenRowFromPlayers();
            PutCardsToBoard(cards, out Dictionary<int, int> playerDamages);
            ApplyDamageToPlayers(playerDamages);
            
            OnRoundEnded?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteRowTakeAction()
        {
            int chosenRowIndex = playerController.ChosenRow[0];
            int chosenCard = playerController.ChosenRow[1];
            int playerId = playerController.ChosenRow[2];
            boardController.TakeRow(chosenRowIndex, chosenCard, playerId, out Dictionary<int, int> playerDamages);
            ApplyDamageToPlayers(playerDamages);

            var oldCards = Board.Instance.ChosenCards.Clone() as int[][];

            var newCards = oldCards.Skip(1).ToArray();
            boardController.PutChosenCards(newCards);
            ExecutePutAction(newCards);

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