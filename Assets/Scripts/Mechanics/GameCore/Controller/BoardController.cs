using System.Collections.Generic;
using UnityEngine;

namespace AsepStudios.Mechanic.GameCore
{
    public class BoardController
    {
        private readonly Board board;
        public BoardController()
        {
            board = Board.Instance;
        }

        //return player and player hit damage
        //there is no lesser card
        public void PutCards(int[][] chosenCards, out Dictionary<int, int> playerDamages)
        {
            int[][] newBoard = this.board.Values.Clone() as int[][];

            Dictionary<int, int> damages = new();

            for (var index = 0; index < chosenCards.GetLength(0); index++)
            {
                var card = chosenCards[index][0];
                var player = chosenCards[index][1];
                
                int rowIndex = BoardHelper.TryFindRowForCard(card, newBoard);

                if (rowIndex == -1)
                {
                    Debug.LogError("Row index is -1 !!!");
                }
                
                if(BoardHelper.GetCardCountOfRow(rowIndex, newBoard) == 3)
                {

                    var point = BoardHelper.CalculateTotalPointInRow(rowIndex, newBoard);
                    damages.Add(player, point);
                    BoardHelper.ClearRow(rowIndex, newBoard);
                }
                BoardHelper.AddCardToRow(card, rowIndex, newBoard);
            }


            board.SetBoardValues(newBoard);
            playerDamages = damages;
        }
        
        public void PutInitialCards(int[][] initialCards)
        {
            board.SetBoardValues(initialCards);
        }

        public void PutChosenCards(int[][] chosenCards)
        {
            board.SetChosenCards(chosenCards);
        }

        public bool IsThereAnyLesserCardThanBoard(int[][] chosenCards, out int clientId)
        {
            var value = BoardHelper.IsThereAnyLesserCardThanBoard(chosenCards, board.Values, out int id);

            clientId = id;
            return value;
        }
        public bool IsBoardEmpty()
        {
            return BoardHelper.IsBoardEmpty(board.Values);
        }
        
        
    }
}