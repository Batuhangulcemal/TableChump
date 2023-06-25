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
            
            Game.Instance.Board.OnBoardChanged += OnBoardChanged;
            RefreshBoard();
        }

        private void OnDestroy()
        {
            Game.Instance.Board.OnBoardChanged -= OnBoardChanged;
        }

        private void OnBoardChanged(object sender, EventArgs e)
        {
            RefreshChosenCards();
            RefreshBoard();
        }

        private void RefreshChosenCards()
        {
            DestroyService.ClearChildren(chosenCardsTransform);

            for (var index = 0; index < Game.Instance.Board.ChosenCards.Count; index++)
            {
                var cardNumber = Game.Instance.Board.ChosenCards[index];
                var playerClientId = Game.Instance.Board.ChosenCardsPlayers[index];

                string userName = Lobby.Instance.GetPlayerFromClientId(playerClientId).GetUsername();

                Instantiate(cardPrefab, chosenCardsTransform).SetCard(cardNumber, userName);
            }
        }

        private void RefreshBoard()
        {
            ClearBoard();
            
            for (var i = 0; i < boardTransforms.Count; i++)
            {
                for (var j = 0; j < Game.Instance.Board.GetBoard()[i].Count; j++)
                {
                    Instantiate(cardPrefab, boardTransforms[i][j]).SetCard(Game.Instance.Board.GetBoard()[i][j]);
                }
            }
        }

        private void ClearBoard()
        {
            for (var i = 0; i < boardTransforms.Count; i++)
            {
                for (var j = 0; j < boardTransforms[i].Count; j++)
                {
                    DestroyService.ClearChildren(boardTransforms[i][j]);
                }
            }
        }
    }
}