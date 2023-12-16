using AsepStudios.TableChump.Mechanics.GameCore.Controller;
using AsepStudios.TableChump.Mechanics.LobbyCore;
using AsepStudios.TableChump.UI.Component.Custom;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.TableChump.UI.Views.GameScene
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