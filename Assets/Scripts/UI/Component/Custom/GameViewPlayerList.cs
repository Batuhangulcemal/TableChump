using AsepStudios.Mechanic.LobbyCore;
using AsepStudios.Utils;
using UnityEngine;

namespace AsepStudios.UI
{
    public class GameViewPlayerList : MonoBehaviour
    {
        [SerializeField] private GamePlayerRect gamePlayerRectPrefab;
        
        public void RefreshPlayerList()
        {
            DestroyService.ClearChildren(transform);

            foreach (var player in Lobby.Instance.Players)
            {
                Instantiate(gamePlayerRectPrefab, transform).SetGamePlayerRect(player);
            }
        }
    }
}