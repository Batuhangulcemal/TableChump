using AsepStudios.Mechanic.PlayerCore;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class LobbyPlayerRect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI userNameText;
        [SerializeField] private TextMeshProUGUI readyText;
        [SerializeField] private Outline outline;

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
            readyText.text = player.GetReady() ? "Ready" : "Not Ready";
            outline.effectColor = player.GetReady() ? ResourceProvider.Colors.Green : ResourceProvider.Colors.Red;
        }
    }
}

