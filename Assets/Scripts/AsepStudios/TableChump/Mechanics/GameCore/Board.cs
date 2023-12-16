using System;
using AsepStudios.Utils;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.Mechanic.GameCore
{
    public class Board : NetworkBehaviour
    {
        public event EventHandler OnBoardChanged;
        public event EventHandler OnChosenCardsChanged;
        public static Board Instance { get; private set; }

        private readonly NetworkVariable<FixedString512Bytes> values = new(new []
        {
            new [] {-1, -1, -1, -1},
            new [] {-1, -1, -1, -1},
            new [] {-1, -1, -1, -1},
            new [] {-1, -1, -1, -1}
            
        }.SerializeArray());
        
        private readonly NetworkVariable<FixedString512Bytes> chosenCards = new();

        public int[][] Values => GetValues();
        public int[][] ChosenCards => GetChosenCards();
        
        private void Awake()
        {
            Instance = this;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            values.OnValueChanged += Board_OnValuesChanged;
            chosenCards.OnValueChanged += ChosenCards_OnValuesChanged;
            
        }

        internal void SetBoardValues(int[][] newValues)
        {
            values.Value = newValues.SerializeArray();
        }

        internal void SetChosenCards(int[][] newChosenCards)
        {
            chosenCards.Value = newChosenCards.SerializeArray();
        }

        internal void ClearChosenCards()
        {
            chosenCards.Value.Clear();
        }

        private int[][] GetValues()
        {
            return values.Value.DeserializeArray<int[][]>();
        }

        private int[][] GetChosenCards()
        {
            return chosenCards.Value.DeserializeArray<int[][]>();
        }
        
        private void Board_OnValuesChanged(FixedString512Bytes previousValues, FixedString512Bytes newValues)
        {
            OnBoardChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private void ChosenCards_OnValuesChanged(FixedString512Bytes previousValues, FixedString512Bytes newValues)
        {
            OnChosenCardsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}