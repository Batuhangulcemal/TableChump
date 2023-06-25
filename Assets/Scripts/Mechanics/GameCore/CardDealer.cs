using System.Collections.Generic;
using System.Linq;
using AsepStudios.Mechanic.PlayerCore;
using AsepStudios.Utils;

namespace AsepStudios.Mechanic.GameCore
{
    public class CardDealer
    {
        private static readonly List<int> Cards = Enumerable.Range(1, 199).ToList();

        public static void DealCards(List<Player> players, Board board)
        {
            Cards.Shuffle();
            DealCardsToPlayers(players);
            DealCardsToBoard(board);
        }

        private static void DealCardsToPlayers(List<Player> players)
        {
            for (var index = 0; index < players.Count; index++)
            {
                var player = players[index].GamePlayer;
                player.SetCards(Cards.GetRange(index * 10, 10));
            }
        }

        private static void DealCardsToBoard(Board board)
        {
            board.PutInitialCards(Cards.TakeLast(4).ToList());
        }
    }
}