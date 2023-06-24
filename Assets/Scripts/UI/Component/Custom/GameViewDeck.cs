using System;
using System.Collections.Generic;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;
using AsepStudios.Utils;
using UnityEngine;

namespace AsepStudios.UI
{
    public class GameViewDeck  : MonoBehaviour
    {
        [SerializeField] public Card cardPrefab;

        private readonly List<Card> cards = new();
        private Card chosenCard;
        public void Initialize()
        {
            LocalPlayer.Instance.Player.GamePlayer.OnCardsChanged += OnCardsChanged;
            RefreshDeck();
            AssignOnClickEvents();
        }

        private void OnCardsChanged(object sender, EventArgs e)
        {
            RefreshDeck();
            AssignOnClickEvents();
        }
        
        private void RefreshDeck()
        {
            DestroyService.ClearChildren(transform);
            cards.Clear();

            foreach (var number in LocalPlayer.Instance.Player.GamePlayer.GetCards())
            {
                var card = Instantiate(cardPrefab, transform).SetCard(number);
                cards.Add(card);
            }
        }
        
        private void AssignOnClickEvents()
        {
            foreach (var card in cards)
            {
                card.Interactable = true;
                card.OnClick.AddListener(() =>
                {
                    if (chosenCard == card)
                    {
                        card.SetOn(false);
                        SetChosenCard(null);
                    }
                    else
                    {
                        SetOffAllCards();
                        card.SetOn(true);
                        SetChosenCard(card);
                    }
                });
            }
        }

        private void SetOffAllCards()
        {
            foreach (var card in cards)
            {
                card.SetOn(false);
            }
        }

        private void SetChosenCard(Card card)
        {
            chosenCard = card;
            LocalPlayer.Instance.Player.GamePlayer.ChooseCardServerRpc(card == null ? 0 : card.Number);
        }
        
        
    }
}