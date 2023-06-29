using System;
using System.Linq;
using AsepStudios.Mechanic.LobbyCore;
using AsepStudios.Mechanic.PlayerCore;
using AsepStudios.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace AsepStudios.Mechanic.GameCore
{
    public class PlayerController
    {
        public event EventHandler OnAnyPlayerChosenCardChanged;
        public event EventHandler OnAnyPlayerChosenRowChanged;

        public int[][] ChosenCards => GetChosenCardsSorted();
        
        /// <summary>
        /// first element is rowIndex
        /// second element is cardNumber
        /// third element is playerId
        /// </summary>
        public int[] ChosenRow => GetChosenRow();
        public bool IsEveryoneChoseCard => CheckEveryPlayerChooseACard();
        public bool IsRightPlayerChoseRow => CheckTheRightPlayerChooseARow();
        public bool IsEveryoneAboveZero => CheckEveryPlayerAboveZeroHealth();
        public bool IsPlayerCardsRunOut => CheckPlayerCardsRunOut();
        
        public PlayerController()
        {
            foreach (var player in Lobby.Instance.Players)
            {
                player.GamePlayer.OnChosenCardChanged += GamePlayer_OnAnyPlayerChosenCardChanged;
            }

            foreach (var player in Lobby.Instance.Players)
            {
                player.GamePlayer.OnChosenRowChanged += GamePlayer_OnAnyPlayerChosenRowChanged;
            }
        }
        
        public void RemoveChosenCardsFromPlayers()
        {
            foreach (var player in Lobby.Instance.Players)
            {
                player.GamePlayer.UseChosenCard();
            }
        }

        public void ResetChosenRowFromPlayers()
        {
            foreach (var player in Lobby.Instance.Players)
            {
                player.GamePlayer.ResetChosenRow();
            }
        }


        private int[][] GetChosenCardsSorted()
        {
            var list = Lobby.Instance.Players;

            int[][] result = new int[list.Count][];
            
            for (var index = 0; index < list.Count; index++)
            {
                var player = list[index];
                result[index] = new[]
                {
                    player.GamePlayer.ChosenCard,
                    (int)player.OwnerClientId
                };
            }
                
            result.Sort();
            BoardHelper.PrintBoard(result);
            return result;
        }
        
        private int[] GetChosenRow()
        {
            int[] result = new int[3];
            var rightPlayer = Round.Instance.RowChoosePlayer;
            var player = Lobby.Instance.GetPlayerFromClientId((ulong)rightPlayer);
            
            result[0] = player.GamePlayer.ChosenRow;
            result[1] = player.GamePlayer.ChosenCard;
            result[2] = (int)player.OwnerClientId;            
            return result;
        }
        
        private bool CheckEveryPlayerChooseACard()
        {
            foreach (var player in Lobby.Instance.Players)
            {
                if (!player.GamePlayer.IsChoseCard)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckTheRightPlayerChooseARow()
        {
            var rightPlayer = Round.Instance.RowChoosePlayer;
            var player = Lobby.Instance.GetPlayerFromClientId((ulong)rightPlayer);

            return player.GamePlayer.IsChoseRow;
        }
        
        private bool CheckEveryPlayerAboveZeroHealth()
        {
            foreach (var player in Lobby.Instance.Players)
            {
                if (player.GamePlayer.Point <= 0)
                {
                    return false;
                }
            }
            return true;
        }
        
        private bool CheckPlayerCardsRunOut()
        {
            return Lobby.Instance.Players[0].GamePlayer.IsCardsEmpty;
        }
        
        private void GamePlayer_OnAnyPlayerChosenCardChanged(object sender, EventArgs e)
        {
            OnAnyPlayerChosenCardChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private void GamePlayer_OnAnyPlayerChosenRowChanged(object sender, EventArgs e)
        {
            OnAnyPlayerChosenRowChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}