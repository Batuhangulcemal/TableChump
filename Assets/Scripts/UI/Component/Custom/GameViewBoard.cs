using System;
using System.Collections.Generic;
using AsepStudios.Mechanic.GameCore;
using AsepStudios.Mechanic.LobbyCore;
using AsepStudios.Utils;
using UnityEngine;

namespace AsepStudios.UI
{
    public class GameViewBoard : MonoBehaviour
    {
        [SerializeField] public Card cardPrefab;

        [SerializeField] private List<Transform> firstRowTransform;
        [SerializeField] private List<Transform> secondRowTransform;
        [SerializeField] private List<Transform> thirdRowTransform;
        [SerializeField] private List<Transform> fourthRowTransform;

        [SerializeField] private Transform chosenCardsTransform;

        private List<List<Transform>> boardTransforms;

        public void Initialize()
        {
            boardTransforms = new List<List<Transform>>()
            {
                firstRowTransform,
                secondRowTransform,
                thirdRowTransform,
                fourthRowTransform
            };
            
            Board.Instance.OnBoardChanged += OnBoardChanged;
            RefreshBoard();
        }

        private void OnDestroy()
        {
            Board.Instance.OnBoardChanged -= OnBoardChanged;
        }

        private void OnBoardChanged(object sender, EventArgs e)
        {
            RefreshChosenCards();
            RefreshBoard();
        }

        private void RefreshChosenCards()
        {
            DestroyService.ClearChildren(chosenCardsTransform);

            for (var index = 0; index < Board.Instance.ChosenCards.GetLength(1); index++)
            {
                var cardNumber = Board.Instance.ChosenCards[index][0];
                var playerClientId = Board.Instance.ChosenCards[index][1];

                string userName = Lobby.Instance.GetPlayerFromClientId((ulong)playerClientId).GetUsername();

                Instantiate(cardPrefab, chosenCardsTransform).SetCard(cardNumber, userName);
            }
        }

        private void RefreshBoard()
        {
            ClearBoard();
            
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    var value = Board.Instance.Values[i][j];
                    if (value != 0)
                    {
                        Instantiate(cardPrefab, boardTransforms[i][j]).SetCard(value);
                    }
                }
            }
        }

        private void ClearBoard()
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    DestroyService.ClearChildren(boardTransforms[i][j]);
                }
            }
        }
    }
}