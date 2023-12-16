using AsepStudios.Mechanic.GameCore;
using AsepStudios.Mechanic.LobbyCore;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class GameOverView : View
    {
        [SerializeField] private TextMeshProUGUI waitingHostToRestartGameText;
        [SerializeField] private Button restartGameButton;
        [SerializeField] private Button logoutButton;

        [SerializeField] private GameViewQuitPanel quitPanel;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            quitPanel.Initialize();
            
            if (Lobby.Instance.IsHostPlayerActive)
            {
                restartGameButton.gameObject.SetActive(true);
                restartGameButton.onClick.AddListener(() =>
                {
                    ServerGameController.Controller.RestartGame();
                });
            }
            else
            {
                waitingHostToRestartGameText.gameObject.SetActive(true);
            }
            
            logoutButton.onClick.AddListener(() =>
            {
                quitPanel.ChangeState();
            });
        }


        private void CreateScoreboard()
        {
            
        }
    }
}