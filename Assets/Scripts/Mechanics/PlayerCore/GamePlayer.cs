using System;
using System.Linq;
using AsepStudios.Mechanic.GameCore;
using AsepStudios.Mechanic.GameCore.Enum;
using AsepStudios.Utils;
using Unity.Collections;
using Unity.Netcode;


namespace AsepStudios.Mechanic.PlayerCore
{
    public class GamePlayer : NetworkBehaviour
    {
        public event EventHandler OnCardsChanged; 
        public event EventHandler OnPointChanged;
        public event EventHandler OnChosenCardChanged;
        public event EventHandler OnChosenRowChanged;

        private readonly NetworkVariable<FixedString512Bytes> cards = new();
        private readonly NetworkVariable<int> point = new(99);
        private readonly NetworkVariable<int> chosenCard = new(-1);
        private readonly NetworkVariable<int> chosenRow = new(-1);
        

        public int[] Cards => GetCards();
        public int Point => point.Value;
        public int ChosenCard => chosenCard.Value;
        public int ChosenRow => chosenRow.Value;
        public bool IsChoseCard => chosenCard.Value != -1;
        public bool IsChoseRow => chosenRow.Value != -1;
        public bool IsCardsEmpty => Cards.Length == 0;

        
        public override void OnNetworkSpawn()
        {
            cards.OnValueChanged += CardsOnValueChanged;
            point.OnValueChanged += PointOnValueChanged;
            chosenCard.OnValueChanged += ChosenCardOnValueChanged;
            chosenRow.OnValueChanged += ChosenRowOnValueChanged;
        }
        
        public void DecreasePointByCardNumber(int cardNumber)
        {
            int decreasingPoint = CardPointCalculator.Calculate(cardNumber);
            point.Value -= decreasingPoint;
        }

        public void SetPoint(int value)
        {
            point.Value = value;
        }
        
        public void SetCards(int[] newCards)
        {
            cards.Value = newCards.SerializeArray();
        }

        private int[] GetCards()
        {
            var currentCards = cards.Value.DeserializeArray<int[]>();
            
            if (currentCards != null)
            {
                return currentCards;
            }

            return new int[]{};
        }
        
        [ServerRpc]
        public void ChooseCardServerRpc(int number)
        {
            if (Round.Instance.RoundState == RoundState.WaitingForPlayers)
            {
                chosenCard.Value = number;
            }
        }
        
        [ServerRpc]
        public void ChooseRowServerRpc(int rowIndex)
        {
            if (IsChoseCard)
            {
                chosenRow.Value = rowIndex;
            }
        }

        public void UseChosenCard()
        {
            int[] newCards = Cards.Clone() as int[];
            newCards = newCards.Where(val => val != chosenCard.Value).ToArray();
            
            SetCards(newCards);

            chosenCard.Value = -1;

        }

        public void ResetChosenRow()
        {
            chosenRow.Value = -1;
        }
        
        
        private void CardsOnValueChanged(FixedString512Bytes previousValue, FixedString512Bytes newValue)
        {
            OnCardsChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private void PointOnValueChanged(int previousValue, int newValue)
        {
            OnPointChanged?.Invoke(this,EventArgs.Empty);
        }
        
        private void ChosenCardOnValueChanged(int previousValue, int newValue)
        {
            OnChosenCardChanged?.Invoke(this,EventArgs.Empty);
        }
        
        private void ChosenRowOnValueChanged(int previousValue, int newValue)
        {
            OnChosenRowChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}