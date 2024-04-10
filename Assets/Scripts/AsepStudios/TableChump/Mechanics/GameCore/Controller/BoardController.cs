using System.Collections.Generic;
using AsepStudios.TableChump.Mechanics.GameCore.Helper;
using UnityEngine;

namespace AsepStudios.TableChump.Mechanics.GameCore.Controller
{
    public class BoardController
    {
        private int[][] _originalBoard = {
            new[] {-1, -1, -1 , -1},
            new[] {-1, -1, -1 , -1},
            new[] {-1, -1, -1 , -1},
            new[] {-1, -1, -1 , -1}
        };
        public int[][] OriginalChosenCards;

        //return player and player hit damage
        //there is no lesser card
        public void PutCards(int[][] chosenCards, out Dictionary<int, int> playerDamages)
        {
            int[][] newBoard = _originalBoard.Clone() as int[][];

            Dictionary<int, int> damages = new();

            foreach (var chosen in chosenCards)
            {
                var card = chosen[0];
                var player = chosen[1];
                
                var rowIndex = BoardHelper.TryFindRowForCard(card, newBoard);

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
            
            SetBoard(newBoard);
            playerDamages = damages;
        }

        public void TakeRow(int rowIndex, int card, int playerId, out Dictionary<int, int> playerDamages)
        {
            int[][] newBoard = _originalBoard.Clone() as int[][];

            Dictionary<int, int> damages = new();
            var point = BoardHelper.CalculateTotalPointInRow(rowIndex, newBoard);
            damages.Add(playerId, point);
            playerDamages = damages;
            
            BoardHelper.ClearRow(rowIndex, newBoard);
            BoardHelper.AddCardToRow(card, rowIndex, newBoard);
            
            SetBoard(newBoard);
        }
        
        public void PutInitialCards(int[][] initialCards)
        {
            EmptyChosenCards();
            SetBoard(initialCards);
        }

        public void PutChosenCards(int[][] chosenCards)
        {
            SetChosenCards(chosenCards);
        }

        public bool IsThereAnyLesserCardThanBoard(int[][] chosenCards, out int playerId)
        {
            var value = BoardHelper.IsThereAnyLesserCardThanBoard(chosenCards, _originalBoard, out int id);

            playerId = id;
            return value;
        }
        
        private void SetBoard(int[][] newBoard)
        {
            _originalBoard = newBoard;
            Board.Instance.SetBoardValues(_originalBoard);
        }

        private void SetChosenCards(int[][] chosenCards)
        {
            OriginalChosenCards = chosenCards;
            Board.Instance.SetChosenCards(OriginalChosenCards);
        }

        private void EmptyChosenCards()
        {
            Board.Instance.ClearChosenCards();
        }
        
        
    }
}