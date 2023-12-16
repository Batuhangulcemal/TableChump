using AsepStudios.TableChump.Mechanics.PlayerCore.LocalPlayerCore;

namespace AsepStudios.TableChump.UI.Views.GameScene
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