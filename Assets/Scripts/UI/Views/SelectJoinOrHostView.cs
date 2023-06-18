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

            });

            hostButton.onClick.AddListener(() =>
            {

            });

            backButton.onClick.AddListener(() =>
            {
                ViewManager.ShowView<MainMenuView>();
            });
        }
    }
}

