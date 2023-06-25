using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private NetworkList<int> firstRow;
        private NetworkList<int> secondRow;
        private NetworkList<int> thirdRow;
        private NetworkList<int> fourthRow;

        private NetworkList<int> chosenCards;
        private NetworkList<ulong> chosenCardsPlayers;
        
        public List<NetworkList<int>> board => GetBoard();
        public NetworkList<int> ChosenCards => chosenCards;
        public NetworkList<ulong> ChosenCardsPlayers => chosenCardsPlayers;

        private void Awake()
        {
            firstRow = new();
            secondRow = new();
            thirdRow = new();
            fourthRow = new();
            chosenCards = new();
            chosenCardsPlayers = new();
        }

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
            
            chosenCards.Clear();
            ChosenCardsPlayers.Clear();
            
            StartCoroutine(CallOnBoardChanged());

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
                //this code is temporary
                var totalPoint = CalculateTotalPointInRow(0);
                gamePlayer.DecreasePoint(totalPoint);
                board[0].Clear();
                board[0].Add(card);
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
                StartCoroutine(CallOnBoardChanged());

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

            foreach (var cardNumber in board[rowIndex])
            {
                //calculate point from number 
                result += CardPointCalculator.Calculate(cardNumber);
            }

            return result;
        }

        private IEnumerator CallOnBoardChanged()
        {
            yield return new WaitForSeconds(0.5f);
            CallOnBoardChangedClientRpc();
        }
    
        [ClientRpc]
        private void CallOnBoardChangedClientRpc()
        {
            OnBoardChanged?.Invoke(this,EventArgs.Empty);
        }
        
        
    }
}