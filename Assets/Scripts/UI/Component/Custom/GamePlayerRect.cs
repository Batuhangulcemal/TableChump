using AsepStudios.Mechanic.PlayerCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class GamePlayerRect : MonoBehaviour
    {
        [SerializeField] private Image avatarImage;
        [SerializeField] private TextMeshProUGUI usernameText;
        [SerializeField] private TextMeshProUGUI pointText;

        private Player player;
        
        public void SetGamePlayerRect(Player player)
        {
            this.player = player;

            avatarImage.sprite = ResourceProvider.GetAvatarFromIndex(player.GetAvatarIndex());
            usernameText.text = player.GetUsername();
            pointText.text = player.GamePlayer.GetPoint().ToString();
        }
    }
}