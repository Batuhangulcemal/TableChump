using System.Collections.Generic;
using AsepStudios.App;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class ProfileView : View
    {
        [SerializeField] private Transform profileAvatarButtonsTransform;
        [SerializeField] private ProfileAvatarButton profileAvatarButtonBasePrefab;
        [SerializeField] private TMP_InputField usernameInputField;

        [SerializeField] private Button backButton;

        private bool isGoingToPlayGame;
        private List<ProfileAvatarButton> avatarButtons = new();
        private int chosenAvatarIndex = -1;

        protected override void OnEnable()
        {
            base.OnEnable();

            backButton.onClick.AddListener(() =>
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
