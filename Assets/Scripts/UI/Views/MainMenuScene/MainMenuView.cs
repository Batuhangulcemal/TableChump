using AsepStudios.App;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class MainMenuView : View
    {
        [Header("Main Panel")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button profileButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;

        [Header("Profile Panel")]
        [SerializeField] private Transform profilePanel;
        [SerializeField] private Image avatarImage;
        [SerializeField] private TextMeshProUGUI usernameText;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            SetProfilePanel();

            playButton.onClick.AddListener(() =>
            {
                if (Session.IsInitialized)
                {
                    ViewManager.ShowView<SelectJoinOrHostView>();
                }
                else
                {
                    ViewManager.ShowView<ProfileView>(true);
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


        private void SetProfilePanel()
        {
            if (TryFetchProfileInfo(out string username, out int avatarIndex))
            {
                profilePanel.gameObject.SetActive(true);
                usernameText.text = username;
            }
            else
            {
                profilePanel.gameObject.SetActive(false);
            }
        }

        private bool TryFetchProfileInfo(out string username, out int avatarIndex)
        {
            username = string.Empty;
            avatarIndex = 0;
            if (Session.IsInitialized)
            {
                username = Session.Username;
                avatarIndex = Session.AvatarIndex;
                return true;
            }

            return false;
        }
    }
}
