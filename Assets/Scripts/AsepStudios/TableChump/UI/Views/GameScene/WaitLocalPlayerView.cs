using System;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;
using UnityEngine.SocialPlatforms;

namespace AsepStudios.UI
{
    public class WaitLocalPlayerView : View
    {
        private bool isPlayerSpawned = false;
        private void Update()
        {
            if (!isPlayerSpawned)
            {
                if (LocalPlayer.Instance.Player != null)
                {
                    isPlayerSpawned = true;
                    ViewManager.ShowView<LobbyView>();
                }
            }
            
        }
    }
}