using System;
using System.Collections;
using AsepStudios.Mechanic.PlayerCore;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.Mechanic.GameCore
{
    public class Board : NetworkBehaviour
    {
        public event EventHandler OnBoardChanged;
        public event EventHandler OnChosenCardsChanged;
        public static Board Instance { get; private set; }

        private NetworkVariable<int[][]> values;
        private NetworkVariable<int[][]> chosenCards;

        public int[][] Values => values.Value;
        public int[][] ChosenCards => chosenCards.Value;
        
        private void Awake()
        {
            Instance = this;

            values = new();
            chosenCards = new();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            values.OnValueChanged += Board_OnValuesChanged;
            chosenCards.OnValueChanged += ChosenCards_OnValuesChanged;
        }

        internal void SetBoardValues(int[][] newValues)
        {
            values.Value = newValues;
        }

        internal void SetChosenCards(int[][] newChosenCards)
        {
            chosenCards.Value = newChosenCards;
        }
        
        private void Board_OnValuesChanged(int[][] previousValues, int[][] newValues)
        {
            OnBoardChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private void ChosenCards_OnValuesChanged(int[][] previousValues, int[][] newValues)
        {
            OnChosenCardsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}