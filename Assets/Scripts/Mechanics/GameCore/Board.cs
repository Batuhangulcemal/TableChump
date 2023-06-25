using System;
using System.Collections.Generic;
using System.Linq;
using AsepStudios.Mechanic.LobbyCore;
using AsepStudios.Mechanic.PlayerCore;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.Mechanic.GameCore
{
    public class Board : NetworkBehaviour
    {
        public event EventHandler OnBoardChanged;

        private readonly NetworkList<int> firstRow = new();
        private readonly NetworkList<int> secondRow = new();
        private readonly NetworkList<int> thirdRow = new();
        private readonly NetworkList<int> fourthRow = new();

        private readonly NetworkList<int> chosenCards = new();
        private readonly NetworkList<ulong> chosenCardsPlayers = new();
        
        public List<NetworkList<int>> board => GetBoard();
        public NetworkList<int> ChosenCards => chosenCards;
        public NetworkList<ulong> ChosenCardsPlayers => chosenCardsPlayers;
        
        public void Initialize()
        {
            CardDealer.DealCards(Lobby.Instance.GetPlayers(), this);
            SubscribePlayersCardAction();
        }

        public void Reset()
        {
            UnsubscribePlayersCardAction();
        }

        public void PutInitialCards(List<int> initialCards)
        {
            for (var index = 0; index < board.Count; index++)
            {
                board[index].Clear();
                board[index].Add(initialCards[index]);
            }
            OnBoardChanged?.Invoke(this,EventArgs.Empty);
        }

        public List<NetworkList<int>> GetBoard()
        {
            return new List<NetworkList<int>>()
            {
                firstRow,
                secondRow,
                thirdRow,
                fourthRow
            };
        }
        
        
        private void PutCard(int card, GamePlayer gamePlayer)
        {
            int rowIndex = TryFindRowForCard(card);

            if (rowIndex != -1)
            {
                if (board[rowIndex].Count == 3)
                {
                    //player take hit
                    var totalPoint = CalculateTotalPointInRow(rowIndex);
                    gamePlayer.DecreasePoint(totalPoint);
                    
                    //
                    board[rowIndex].Clear();

                }
                board[rowIndex].Add(card);
            }
            else
            {
                //let player choose which row to take
            }
        }

        private void SubscribePlayersCardAction()
        {
            foreach (var player in Lobby.Instance.GetPlayers())
            {
                player.GamePlayer.OnChosenCardChanged += OnAnyPlayerChosenCardChanged;
            }
        }
        
        private void UnsubscribePlayersCardAction()
        {
            foreach (var player in Lobby.Instance.GetPlayers())
            {
                player.GamePlayer.OnChosenCardChanged -= OnAnyPlayerChosenCardChanged;
            }
        }
        

        private void OnAnyPlayerChosenCardChanged(object sender, EventArgs e)
        {
            if (CheckEveryPlayerChooseACard())
            {
                //Put cards to board with order
                Dictionary<int, GamePlayer> cards = GetChosenCards();
                
                chosenCards.Clear();
                chosenCardsPlayers.Clear();


                foreach (var chosenCard in cards.OrderBy(x => x.Key))
                {
                    chosenCards.Add(chosenCard.Key);
                    chosenCardsPlayers.Add(chosenCard.Value.OwnerClientId);
                    PutCard(chosenCard.Key, chosenCard.Value);
                }
                
                //Remove chosen cards from players
                RemoveChosenCardsFromPlayers();
                OnBoardChanged?.Invoke(this,EventArgs.Empty);

            }
        }

        private Dictionary<int, GamePlayer> GetChosenCards()
        {
            Dictionary<int, GamePlayer> result = new();
            
            foreach (var player in Lobby.Instance.GetPlayers())
            {
                result.Add(player.GamePlayer.GetChosenCard(), player.GamePlayer);
            }

            return result;
        }

        private void RemoveChosenCardsFromPlayers()
        {
            foreach (var player in Lobby.Instance.GetPlayers())
            {
                player.GamePlayer.UseChosenCard();
            }
        }

        private bool CheckEveryPlayerChooseACard()
        {
            foreach (var player in Lobby.Instance.GetPlayers())
            {
                if (!player.GamePlayer.IsChoseCard)
                {
                    return false;
                }
            }

            return true;
        }

        private int TryFindRowForCard(int card)
        {
            int rowIndex = -1;

            int closestNumber = -1;
            
            for (int index = 0; index < 4; index++)
            {
                int edgeCard = board[index][board[index].Count - 1];
                
                if (edgeCard < card)
                {
                    if (edgeCard > closestNumber)
                    {
                        closestNumber = edgeCard;
                        rowIndex = index;
                    }
                }
            }
            return rowIndex;
        }

        private int CalculateTotalPointInRow(int rowIndex)
        {
            int result = 0;

            foreach (var card in board[rowIndex])
            {
                //calculate point from number 
                
                //this is tmp
                result += card;
            }

            return result;
        }
        
        
    }
}