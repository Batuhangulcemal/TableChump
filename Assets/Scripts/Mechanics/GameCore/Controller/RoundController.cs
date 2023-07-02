using System;
using System.Collections.Generic;
using System.Linq;
using AsepStudios.Mechanic.GameCore.Enum;
using AsepStudios.Mechanic.LobbyCore;
using UnityEngine;

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
            round.ChangeRoundState(RoundState.Dealing);
            CardDealer.DealCards(boardController);
            round.ChangeRoundState(RoundState.WaitingForPlayers);
            
        }
        
        private void PlayerController_OnOnAnyPlayerChosenCardChanged(object sender, EventArgs e)
        {
            if (PlayerController.IsEveryoneChoseCard)
            {
                OnEveryoneChose();

            }
        }
        
        private void PlayerController_OnAnyPlayerChosenRowChanged(object sender, EventArgs e)
        {
            if (round.RoundState != RoundState.WaitingForPlayerChooseARow) return;

            if (!PlayerController.IsRightPlayerChoseRow) return;
            
            //right player chose a row.
            
            ExecuteRowTakeAction();
        }
        
        private void OnEveryoneChose()
        {
            if (round.RoundState != RoundState.WaitingForPlayers) return;
            //Put cards to board with order
            int[][] cards = PlayerController.ChosenCards;
            
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
            round.ChangeRowChoosePlayer(-1);
            round.ChangeRoundState(RoundState.Animating);
            PlayerController.RemoveChosenCardsFromPlayers();
            PlayerController.ResetChosenRowFromPlayers();
            PutCardsToBoard(cards);

            OnActionsEnd();
        }

        private void ExecuteRowTakeAction()
        {
            TakeRowFromBoard();
            
            var oldCards = boardController.OriginalChosenCards.Clone() as int[][];
            var newCards = oldCards.Skip(1).ToArray();
            boardController.PutChosenCards(newCards);
            
            ExecutePutAction(newCards);

        }

        private void PutCardsToBoard(int[][] cards)
        {
            boardController.PutCards(cards, out Dictionary<int, int> playerDamages);
            ApplyDamageToPlayers(playerDamages);
        }

        private void TakeRowFromBoard()
        {
            int[] data = PlayerController.ChosenRow;
            int chosenRowIndex = data[0];
            int chosenCard = data[1];
            int playerId = data[2];

            boardController.TakeRow(chosenRowIndex, chosenCard, playerId, out Dictionary<int, int> playerDamages);
            ApplyDamageToPlayers(playerDamages);
        }

        private void ApplyDamageToPlayers(Dictionary<int, int> playerDamages)
        {
            foreach (var damages in playerDamages)
            {
                Lobby.Instance.GetPlayerFromClientId((ulong)damages.Key).GamePlayer.DecreasePoint(damages.Value);
            }
        }
        
        private void OnActionsEnd()
        {
            if (PlayerController.IsPlayerCardsRunOut)
            {
                OnRoundEnded?.Invoke(this,EventArgs.Empty);
            }
            else
            {
                round.ChangeRoundState(RoundState.WaitingForPlayers);
            }
        }
    }
}