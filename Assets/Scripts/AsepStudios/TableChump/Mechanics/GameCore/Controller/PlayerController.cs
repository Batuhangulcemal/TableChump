using System;
using AsepStudios.TableChump.Mechanics.GameCore.Helper;
using AsepStudios.TableChump.Mechanics.LobbyCore;
using AsepStudios.TableChump.Utils.Service;

namespace AsepStudios.TableChump.Mechanics.GameCore.Controller
{
    public class PlayerController
    {
        public event Action OnAnyPlayerChosenCardChanged;
        public event Action OnAnyPlayerChosenRowChanged;

        public static int[][] ChosenCards => GetChosenCardsSorted();
        
        /// <summary>
        /// first element is rowIndex
        /// second element is cardNumber
        /// third element is playerId
        /// </summary>
        public static int[] ChosenRow => GetChosenRow();
        public static bool IsEveryoneChoseCard => CheckEveryPlayerChooseACard();
        public static bool IsRightPlayerChoseRow => CheckTheRightPlayerChooseARow();
        public static bool IsEveryoneAboveZero => CheckEveryPlayerAboveZeroHealth();
        public static bool IsPlayerCardsRunOut => CheckPlayerCardsRunOut();
        
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
        
        public static void RemoveChosenCardsFromPlayers()
        {
            foreach (var player in Lobby.Instance.Players)
            {
                player.GamePlayer.UseChosenCard();
            }
        }

        public static void ResetChosenRowFromPlayers()
        {
            foreach (var player in Lobby.Instance.Players)
            {
                player.GamePlayer.ResetChosenRow();
            }
        }

        public static void SetPlayersPoint(int value)
        {
            foreach (var player in Lobby.Instance.Players)
            {
                player.GamePlayer.SetPoint(value);
            }
        }


        private static int[][] GetChosenCardsSorted()
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
        
        private static int[] GetChosenRow()
        {
            int[] result = new int[3];
            var rightPlayer = Round.Instance.RowChoosePlayer;
            var player = Lobby.Instance.GetPlayerFromClientId((ulong)rightPlayer);
            
            result[0] = player.GamePlayer.ChosenRow;
            result[1] = player.GamePlayer.ChosenCard;
            result[2] = (int)player.OwnerClientId;            
            return result;
        }
        
        private static bool CheckEveryPlayerChooseACard()
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

        private static bool CheckTheRightPlayerChooseARow()
        {
            var rightPlayer = Round.Instance.RowChoosePlayer;
            var player = Lobby.Instance.GetPlayerFromClientId((ulong)rightPlayer);

            return player.GamePlayer.IsChoseRow;
        }
        
        private static bool CheckEveryPlayerAboveZeroHealth()
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
        
        private static bool CheckPlayerCardsRunOut()
        {
            return Lobby.Instance.Players[0].GamePlayer.IsCardsEmpty;
        }
        
        private void GamePlayer_OnAnyPlayerChosenCardChanged()
        {
            OnAnyPlayerChosenCardChanged?.Invoke();
        }
        
        private void GamePlayer_OnAnyPlayerChosenRowChanged()
        {
            OnAnyPlayerChosenRowChanged?.Invoke();
        }

    }
}