using System.Collections.Generic;
using AsepStudios.TableChump.App;
using AsepStudios.TableChump.UI.Component.Custom;
using AsepStudios.TableChump.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.TableChump.UI.Views.MainMenuScene
{
    public class ProfileView : View
    {
        [SerializeField] private Transform profileAvatarButtonsTransform;
        [SerializeField] private ProfileAvatarButton profileAvatarButtonBasePrefab;
        [SerializeField] private TMP_InputField usernameInputField;

        [SerializeField] private Button doneButton;
        [SerializeField] private Button backButton;

        private bool isGoingToPlayGame;
        private readonly List<ProfileAvatarButton> avatarButtons = new();
        private int chosenAvatarIndex = -1;

        protected override void OnEnable()
        {
            base.OnEnable();

            doneButton.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(usernameInputField.text) || chosenAvatarIndex == -1)
                {
                    //TOdo make info text
                }
                else
                {
                    Session.SetSession(usernameInputField.text, chosenAvatarIndex);
                    ShowNextView();
                }
            });
            
            backButton.onClick.AddListener(() =>
            {
                ViewManager.ShowView<MainMenuView>();
            });

            CreateAvatarButtons();
            AssignAvatarButtonOnClicks();
        }
        
        public override void PassArgs(object args = null)
        {
            base.PassArgs(args);

            if(args != null)
            {
                isGoingToPlayGame = true;
            }
        }

        private void ShowNextView()
        {
            if (isGoingToPlayGame)
            {
                ViewManager.ShowView<SelectJoinOrHostView>();
            }
            else
            {
                ViewManager.ShowView<MainMenuView>();
            }
        }
        
        private void CreateAvatarButtons()
        {
            foreach (var sprite in ResourceProvider.Avatars)
            {
                var avatarButton = Instantiate(profileAvatarButtonBasePrefab, profileAvatarButtonsTransform);
                avatarButtons.Add(avatarButton);
                avatarButton.ButtonSprite = sprite;
            }
        }
        
        private void AssignAvatarButtonOnClicks()
        {
            foreach (var button in avatarButtons)
            {
                button.OnClick.AddListener(() =>
                {
                    SetOfAllAvatarButtons();
                    chosenAvatarIndex = ResourceProvider.GetIndexFromSprite(button.ButtonSprite);
                    button.Highlight(true);
                });
            }
        }

        private void SetOfAllAvatarButtons()
        {
            foreach (var button in avatarButtons)
            {
                button.Highlight(false);
            }
        }
    }

}
