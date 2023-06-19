using TMPro;
using UnityEngine;

namespace AsepStudios.UI
{
    public class LobbyPlayerRect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI userNameText;

        public void SetLobbyPlayerRect(string userName)
        {
            userNameText.text = userName;
        }
    }
}

