using AsepStudios.TableChump.App;
using AsepStudios.TableChump.UI.Component.Custom;
using AsepStudios.TableChump.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.TableChump.UI.Views.MainMenuScene
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

        [SerializeField] private MainMenuQuitPanel quitPanel;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            quitPanel.Initialize();
            
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
                quitPanel.ChangeState();
            });
            
            SetProfilePanel();

        }


        private void SetProfilePanel()
        {
            if (TryFetchProfileInfo(out string username, out int avatarIndex))
            {
                profilePanel.gameObject.SetActive(true);
                usernameText.text = username;
                avatarImage.sprite = ResourceProvider.GetAvatarFromIndex(avatarIndex);
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
