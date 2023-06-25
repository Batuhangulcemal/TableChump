using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.Mechanic.PlayerCore
{
    public class GamePlayer : NetworkBehaviour
    {
        public event EventHandler OnCardsChanged; 
        public event EventHandler OnPointChanged;
        public event EventHandler OnChosenCardChanged;
        
        private readonly NetworkVariable<int> point = new();
        private readonly NetworkList<int> cards = new();
        private readonly NetworkVariable<int> chosenCard = new(0);
        public bool IsChoseCard => chosenCard.Value != 0;
        public override void OnNetworkSpawn()
        {
            cards.OnListChanged += CardsOnListChanged;
            point.OnValueChanged += PointOnValueChanged;
            chosenCard.OnValueChanged += ChosenCardOnValueChanged;
        }
        
        public int GetPoint()
        {
            return point.Value;
        }

        public void DecreasePoint(int value)
        {
            point.Value -= value;
        }
        
        public void SetCards(List<int> cards)
        {
            ClearCards();
            point.Value = 80;
            chosenCard.Value = 0;

            foreach (var card in cards)
            {
                this.cards.Add(card);
            }
        }

        public List<int> GetCards()
        {
            var result = new List<int>();
            foreach (var card in cards)
            {
                result.Add(card);
            }
            return result;
        }

        [ServerRpc]
        public void ChooseCardServerRpc(int number)
        {
            chosenCard.Value = number;
        }

        public int GetChosenCard()
        {
            return chosenCard.Value;
        }

        public void UseChosenCard()
        {
            cards.Remove(chosenCard.Value);
            chosenCard.Value = 0;
        }

        public void ClearCards()
        {
            cards.Clear();
        }
        
        private void CardsOnListChanged(NetworkListEvent<int> changeEvent)
        {
            OnCardsChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private void PointOnValueChanged(int previousvalue, int newvalue)
        {
            OnPointChanged?.Invoke(this,EventArgs.Empty);
        }
        
        private void ChosenCardOnValueChanged(int previousvalue, int newvalue)
        {
            OnChosenCardChanged?.Invoke(this,EventArgs.Empty);
        }

    }
}