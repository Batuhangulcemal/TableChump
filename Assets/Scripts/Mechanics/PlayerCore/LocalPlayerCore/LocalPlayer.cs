using System;
using AsepStudios.Mechanic.GameCore;
using AsepStudios.Mechanic.GameCore.Enum;
using UnityEngine;

namespace AsepStudios.Mechanic.PlayerCore.LocalPlayerCore
{
    public class LocalPlayer : MonoBehaviour
    {
        public event EventHandler OnPlayerAttached;
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
            OnPlayerAttached?.Invoke(this, EventArgs.Empty);
            
            Game.Instance.OnGameStateChanged += Game_OnGameStateChanged;
        }
        
        private void Game_OnGameStateChanged(object sender, EventArgs e)
        {
            if (Game.Instance.GameState.Value == GameState.NotStarted)
            {
                Player.SetReadyLocal(false);
            }
        }
    }
}
