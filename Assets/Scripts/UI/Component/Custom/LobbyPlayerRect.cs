using AsepStudios.Mechanic.PlayerCore;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class LobbyPlayerRect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI userNameText;
        [SerializeField] private TextMeshProUGUI identifierText;
        [SerializeField] private Outline outline;
        [SerializeField] private Image avatarImage;
        [SerializeField] private Image rectImage;

        private Player player;

        public void SetLobbyPlayerRect(Player player)
        {
            this.player = player;
            this.player.OnAnyPlayerPropertyChanged += Player_OnAnyPlayerPropertyChanged;

            SetRectFields();

        }

        private void OnDestroy()
        {
            player.OnAnyPlayerPropertyChanged -= Player_OnAnyPlayerPropertyChanged;
        }

        private void Player_OnAnyPlayerPropertyChanged(object sender, EventArgs e)
        {
            SetRectFields();
        }

        private void SetRectFields()
        {
            userNameText.text = player.GetUsername();
            
            outline.effectColor = player.GetReady() ? ResourceProvider.Colors.Green : ResourceProvider.Colors.Red;
            avatarImage.sprite = ResourceProvider.GetAvatarFromIndex(player.GetAvatarIndex());
            
            rectImage.color = player.IsLocalPlayer ?
                ResourceProvider.Colors.Navy : player.IsOwnedByServer ?
                    ResourceProvider.Colors.Orange : ResourceProvider.Colors.Beach;
            
            identifierText.text = player.IsLocalPlayer ?
                "YOU" : player.IsOwnedByServer ?
                    "HOST" : string.Empty;
        }
    }
}

