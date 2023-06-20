using AsepStudios.Mechanic.PlayerCore;
using TMPro;
using UnityEngine;

namespace AsepStudios.UI
{
    public class LobbyPlayerRect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI userNameText;

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

        }
    }
}

