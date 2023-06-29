using System;
using System.Collections.Generic;
using AsepStudios.Mechanic.GameCore;
using AsepStudios.Mechanic.GameCore.Enum;
using AsepStudios.Mechanic.LobbyCore;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;
using AsepStudios.Utils;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField] private Button firstRowButton;
        [SerializeField] private Button secondRowButton;
        [SerializeField] private Button thirdRowButton;
        [SerializeField] private Button fourthRowButton;

        private List<List<Transform>> boardTransforms;
        private List<Button> rowButtons;

        public void Initialize()
        {
            boardTransforms = new List<List<Transform>>()
            {
                firstRowTransform,
                secondRowTransform,
                thirdRowTransform,
                fourthRowTransform
            };

            rowButtons = new List<Button>()
            {
                firstRowButton,
                secondRowButton,
                thirdRowButton,
                fourthRowButton
            };
            
            firstRowButton.onClick.AddListener(() =>
            {
                LocalPlayer.Instance.Player.GamePlayer.ChooseRowServerRpc(0);
            });
            secondRowButton.onClick.AddListener(() =>
            {
                LocalPlayer.Instance.Player.GamePlayer.ChooseRowServerRpc(1);
            });
            thirdRowButton.onClick.AddListener(() =>
            {
                LocalPlayer.Instance.Player.GamePlayer.ChooseRowServerRpc(2);
            });
            fourthRowButton.onClick.AddListener(() =>
            {
                LocalPlayer.Instance.Player.GamePlayer.ChooseRowServerRpc(3);
            });
            
            Board.Instance.OnBoardChanged += OnBoardChanged;
            Board.Instance.OnChosenCardsChanged += OnChosenCardsChanged;
            Round.Instance.OnRoundStateChanged += OnRoundStateChanged;
            RefreshBoard();
            RefreshChosenCards();
            RefreshRowButtons();

        }



        private void OnDestroy()
        {
            Board.Instance.OnBoardChanged -= OnBoardChanged;
            Board.Instance.OnChosenCardsChanged -= OnChosenCardsChanged;
        }

        private void OnBoardChanged(object sender, EventArgs e)
        {
            RefreshBoard();
        }

        private void OnChosenCardsChanged(object sender, EventArgs e)
        {
            RefreshChosenCards();
        }
        
        private void OnRoundStateChanged(object sender, EventArgs e)
        {
            RefreshRowButtons();
        }
        
        private void RefreshBoard()
        {
            ClearBoard();
            BoardHelper.PrintBoard(Board.Instance.Values);

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    var value = Board.Instance.Values[i][j];
                    if (value != -1)
                    {
                        Instantiate(cardPrefab, boardTransforms[i][j]).SetCard(value);
                    }
                }
            }
        }
        
        private void RefreshChosenCards()
        {
            DestroyService.ClearChildren(chosenCardsTransform);

            if (Board.Instance.ChosenCards == null)
            {
                return;
            }

            for (var index = 0; index < Board.Instance.ChosenCards.Length; index++)
            {
                var cardNumber = Board.Instance.ChosenCards[index][0];
                var playerClientId = Board.Instance.ChosenCards[index][1];

                string userName = Lobby.Instance.GetPlayerFromClientId((ulong)playerClientId).GetUsername();

                Instantiate(cardPrefab, chosenCardsTransform).SetCard(cardNumber, userName);
            }
        }

        private void RefreshRowButtons()
        {
            if (Round.Instance.RoundState == RoundState.WaitingForPlayerChooseARow)
            {
                if (Round.Instance.RowChoosePlayer == (int)LocalPlayer.Instance.Player.OwnerClientId)
                {
                    ShowRowButtons();
                }
                else
                {
                    HideRowButtons();
                }
            }
            else
            {
                HideRowButtons();

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


        private void ShowRowButtons()
        {
            foreach (var button in rowButtons)
            {
                button.gameObject.SetActive(true);
            }
        }

        private void HideRowButtons()
        {
            foreach (var button in rowButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
    }
}