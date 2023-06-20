using AsepStudios.App;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class MainMenuView : View
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button profileButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;

        protected override void OnEnable()
        {
            base.OnEnable();

            playButton.onClick.AddListener(() =>
            {
                if (Session.IsInitialized)
                {
                    ViewManager.ShowView<SelectJoinOrHostView>();
                }
                else
                {
                    ViewManager.ShowView<ProfileView>();
                }
            });

            profileButton.onClick.AddListener(() =>
            {
                ViewManager.ShowView<ProfileView>();
            });

            settingsButton.onClick.AddListener(() =>
            {

            });

            quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}
