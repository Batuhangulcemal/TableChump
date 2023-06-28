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

        public int[][] ChosenCards => GetChosenCardsSorted();
        public bool IsEveryoneChose => CheckEveryPlayerChooseACard();

        public PlayerController()
        {
            foreach (var player in Lobby.Instance.Players)
            {
                player.GamePlayer.OnChosenCardChanged += GamePlayer_OnAnyPlayerChosenCardChanged;
            }
        }
        
        public void RemoveChosenCardsFromPlayers()
        {
            foreach (var player in Lobby.Instance.Players)
            {
                player.GamePlayer.UseChosenCard();
            }
        }

        private void GamePlayer_OnAnyPlayerChosenCardChanged(object sender, EventArgs e)
        {
            OnAnyPlayerChosenCardChanged?.Invoke(this, EventArgs.Empty);
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
                    player.GamePlayer.GetChosenCard(),
                    (int)player.OwnerClientId
                };
            }

            var sorted = result.OrderBy(x => x[0]).ToArray();
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
    }
}