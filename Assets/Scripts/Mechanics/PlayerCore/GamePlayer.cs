using System;
using System.Collections.Generic;
using Unity.Netcode;

namespace AsepStudios.Mechanic.PlayerCore
{
    public class GamePlayer : NetworkBehaviour
    {
        public event EventHandler OnCardsChanged; 
        public event EventHandler OnPointChanged; 
        
        private readonly NetworkVariable<int> point = new(0);
        private readonly NetworkList<int> cards = new();

        public override void OnNetworkSpawn()
        {
            cards.OnListChanged += CardsOnListChanged;
            point.OnValueChanged += PointOnValueChanged;

        }

        public int GetPoint()
        {
            return point.Value;
        }
        
        public void SetCards(List<int> cards)
        {
            this.cards.Clear();

            foreach (var card in cards)
            {
                this.cards.Add(card);
            }
        }
        
        private void CardsOnListChanged(NetworkListEvent<int> changeEvent)
        {
            OnCardsChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private void PointOnValueChanged(int previousvalue, int newvalue)
        {
            OnPointChanged?.Invoke(this,EventArgs.Empty);
        }

    }
}