using System.Collections.Generic;
using System.Linq;
using AsepStudios.Mechanic.LobbyCore;
using AsepStudios.Mechanic.PlayerCore;
using AsepStudios.Utils;

namespace AsepStudios.Mechanic.GameCore
{
    public class CardDealer
    {
        private static readonly List<int> Cards = Enumerable.Range(1, 199).ToList();

        public static void DealCards(BoardController boardController)
        {
            Cards.Shuffle();
            DealCardsToPlayers(Lobby.Instance.Players);
            DealCardsToBoard(boardController);
        }

        private static void DealCardsToPlayers(List<Player> players)
        {
            for (var index = 0; index < players.Count; index++)
            {
                var player = players[index].GamePlayer;
                player.SetCards(Cards.GetRange(index * 10, 10).ToArray());
            }
        }

        private static void DealCardsToBoard(BoardController boardController)
        {
            int[] last = Cards.TakeLast(4).ToArray();
            boardController.PutInitialCards(new[]
            {
                new[] {last[0], -1, -1, -1},
                new[] {last[1], -1, -1, -1},
                new[] {last[2], -1, -1, -1},
                new[] {last[3], -1, -1, -1}
                
            });
        }
    }
}