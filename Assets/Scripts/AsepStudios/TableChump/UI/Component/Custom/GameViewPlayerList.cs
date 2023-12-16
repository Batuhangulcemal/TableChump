using AsepStudios.TableChump.Mechanics.LobbyCore;
using AsepStudios.TableChump.Utils.Service;
using UnityEngine;

namespace AsepStudios.TableChump.UI.Component.Custom
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