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

        private Player player;

        public void SetLobbyPlayerRect(Player _player)
        {
            player = _player;
            player.OnAnyPlayerPropertyChanged += Player_OnAnyPlayerPropertyChanged;

            SetRectFields();

        }

        private void Player_OnAnyPlayerPropertyChanged(object sender, System.EventArgs e)
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

