using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class SelectJoinOrHostView : View
    {
        [SerializeField] private Button joinButton;
        [SerializeField] private Button hostButton;
        [SerializeField] private Button backButton;

        protected override void OnEnable()
        {
            base.OnEnable();

            joinButton.onClick.AddListener(() =>
            {
                ViewManager.ShowView<JoinGameView>();
            });

            hostButton.onClick.AddListener(() =>
            {
                ViewManager.ShowView<HostGameView>();
            });

            backButton.onClick.AddListener(() =>
            {
                ViewManager.ShowView<MainMenuView>();
            });
        }
    }
}

