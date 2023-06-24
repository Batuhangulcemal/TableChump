using System.Collections.Generic;
using System.Linq;
using AsepStudios.Mechanic.PlayerCore;
using AsepStudios.Utils;

namespace AsepStudios.Mechanic.GameCore
{
    public class CardDealer
    {
        private static readonly List<int> Cards = Enumerable.Range(1, 200).ToList();
        public static void DealCardsToPlayers(List<GamePlayer> players)
        {
            Cards.Shuffle();
            for (var index = 0; index < players.Count; index++)
            {
                var player = players[index];
                player.SetCards(Cards.GetRange(index * 10, 10));
            }
        }
    }
}