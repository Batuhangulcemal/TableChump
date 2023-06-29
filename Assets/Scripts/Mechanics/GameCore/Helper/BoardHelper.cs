using UnityEngine;

namespace AsepStudios.Mechanic.GameCore
{
    public static class BoardHelper
    {
        public static int TryFindRowForCard(int card, int[][] board)
        {
            int row = -1;

            int closestNumber = -1;
            
            for (int rowIndex = 0; rowIndex < 4; rowIndex++)
            {
                int edgeCard = GetEdgeCardOfRow(rowIndex, board);
                
                if (edgeCard < card)
                {
                    if (edgeCard > closestNumber)
                    {
                        closestNumber = edgeCard;
                        row = rowIndex;
                    }
                }
            }
            return row;
        }

        public static int GetEdgeCardOfRow(int rowIndex, int[][] board)
        {
            int result = -1;

            foreach (var cardNumber in board[rowIndex])
            {
                if (cardNumber > -1)
                {
                    result = cardNumber;
                }
            }
            return result;
        }
        
        public static int GetCardCountOfRow(int rowIndex, int[][] board)
        {
            int count = 0;
            
            foreach (var cardNumber in board[rowIndex])
            {
                if (cardNumber != -1)
                {
                    count++;
                }
            }
            
            return count;
        }
        
        public static int CalculateTotalPointInRow(int rowIndex, int[][] board)
        {
            int result = 0;

            foreach (var cardNumber in board[rowIndex])
            {
                //calculate point from number 
                result += CardPointCalculator.Calculate(cardNumber);
            }

            return result;
        }

        public static void ClearRow(int rowIndex, int[][] board)
        {
            for (var index = 0; index < board[rowIndex].Length; index++)
            {
                board[rowIndex][index] = -1;
            }
        }

        public static void AddCardToRow(int card, int rowIndex, int[][] board)
        {
            int count = GetCardCountOfRow(rowIndex, board);
            board[rowIndex][count] = card;

        }

        public static int GetSmallestEdgeCard(int[][] board)
        {
            int min = 999;

            for (var index = 0; index < board.Length; index++)
            {
                int edgeCard = GetEdgeCardOfRow(index, board);
                if (edgeCard < min)
                {
                    min = edgeCard;
                }
            }

            if (min == 999)
            {
                Debug.LogError("Min is 999!!!");
            }

            return min;
        }
        
        public static bool IsThereAnyLesserCardThanBoard(int[][] chosenCards, int[][] board, out int playerId)
        {
            int smallestEdgeCard = GetSmallestEdgeCard(board);
            playerId = -1;

            PrintBoard(chosenCards);
            
            foreach (var card in chosenCards)
            {
                if (card[0] < smallestEdgeCard)
                {
                    playerId = card[1];
                    return true;
                }
            }
            return false;
            
        }

        public static bool IsBoardEmpty(int[][] board)
        {
            foreach (var row in board)
            {
                if (row[0] != -1)
                {
                    return false;
                }
            }

            return true;
        }

        public static void PrintBoard(int[][] board)
        {
            var log = "Board: \n";
            foreach (var row in board)
            {
                foreach (var card in row)
                {
                    log += card + " ";
                }
                log += "\n";
            }
            Debug.Log(log);
        }
        
        
    }
}