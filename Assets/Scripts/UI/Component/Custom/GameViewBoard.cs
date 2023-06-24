using System;
using System.Collections.Generic;
using AsepStudios.Mechanic.GameCore;
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

        private void OnBoardChanged(object sender, EventArgs e)
        {
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            ClearBoard();
            
            for (var i = 0; i < boardTransforms.Count; i++)
            {
                for (var j = 0; j < Game.Instance.Board.GetBoard()[i].Count; j++)
                {
                    Instantiate(cardPrefab, boardTransforms[i][j], false).SetCard(Game.Instance.Board.GetBoard()[i][j]);
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