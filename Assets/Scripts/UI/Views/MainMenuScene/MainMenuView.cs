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
                ViewManager.ShowView<SelectJoinOrHostView>();
            });

            profileButton.onClick.AddListener(() =>
            {

            });

            settingsButton.onClick.AddListener(() =>
            {

            });

            quitButton.onClick.AddListener(() =>
            {

            });
        }
    }
}
