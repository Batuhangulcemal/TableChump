using System;
using AsepStudios.TableChump.Mechanics.GameCore;
using AsepStudios.TableChump.Mechanics.GameCore.Enum;
using UnityEngine;

namespace AsepStudios.TableChump.Mechanics.PlayerCore.LocalPlayerCore
{
    public class LocalPlayer : MonoBehaviour
    {
        public event Action OnPlayerAttached;
        public static LocalPlayer Instance { get; private set; }
        public Player Player { get; private set; }
        public bool IsPlayerAttached => Player != null;

        private void Awake()
        {
            Instance = this;
        }
        
        public void AttachPlayer(Player player)
        {
            Player = player;
            OnPlayerAttached?.Invoke();
            
            Game.Instance.OnGameStateChanged += Game_OnGameStateChanged;
        }
        
        private void Game_OnGameStateChanged()
        {
            if (Game.Instance.GameState.Value == GameState.NotStarted)
            {
                Player.SetReadyLocal(false);
            }
        }
    }
}
