using AsepStudios.Mechanic.PlayerCore;
using System;
using TMPro;
using UnityEngine;

namespace AsepStudios.UI
{
    public class LobbyPlayerRect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI userNameText;
        [SerializeField] private TextMeshProUGUI readyText;

        private Player player;

        public void SetLobbyPlayerRect(Player player)
        {
            this.player = player;
            this.player.OnAnyPlayerPropertyChanged += Player_OnAnyPlayerPropertyChanged;

            SetRectFields();

        }

        private void Player_OnAnyPlayerPropertyChanged(object sender, EventArgs e)
        {
            SetRectFields();
        }

        private void SetRectFields()
        {
            userNameText.text = player.GetUsername();
            readyText.text = player.GetReady() ? "Ready" : "Not Ready";
        }
    }
}

