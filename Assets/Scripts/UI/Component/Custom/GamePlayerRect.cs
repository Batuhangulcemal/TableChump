using System;
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
            pointText.text = player.GamePlayer.Point.ToString();


            player.GamePlayer.OnPointChanged += Player_OnPointChanged;
            RefreshPoint();
        }
        
        private void Player_OnPointChanged(object sender, EventArgs e)
        {
            RefreshPoint();
        }
        private void RefreshPoint()
        {
            pointText.text = player.GamePlayer.Point.ToString();
        }
    }
}