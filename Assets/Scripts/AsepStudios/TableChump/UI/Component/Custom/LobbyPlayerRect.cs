using System;
using AsepStudios.TableChump.Mechanics.PlayerCore;
using AsepStudios.TableChump.Utils;
using AsepStudios.TableChump.Utils.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.TableChump.UI.Component.Custom
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
            if (player != null)
            {
                player.OnAnyPlayerPropertyChanged -= Player_OnAnyPlayerPropertyChanged;
            }

        }

        private void Player_OnAnyPlayerPropertyChanged(object sender, EventArgs e)
        {
            SetRectFields();
        }

        private void SetRectFields()
        {
            userNameText.text = player.GetUsername();
            
            outline.effectColor = player.GetReady() ? ColorService.Green : ColorService.Red;
            avatarImage.sprite = ResourceProvider.GetAvatarFromIndex(player.GetAvatarIndex());
            
            rectImage.color = player.IsLocalPlayer ?
                ColorService.Navy : player.IsOwnedByServer ?
                    ColorService.Orange : ColorService.Beach;
            
            identifierText.text = player.IsLocalPlayer ?
                "YOU" : player.IsOwnedByServer ?
                    "HOST" : string.Empty;
        }
    }
}

