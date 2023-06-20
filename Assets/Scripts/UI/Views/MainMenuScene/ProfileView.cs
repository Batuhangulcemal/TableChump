using AsepStudios.App;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class ProfileView : View
    {
        [SerializeField] private TMP_InputField usernameInputField;

        [SerializeField] private Button backButton;

        private bool isGoingToPlayGame;

        protected override void OnEnable()
        {
            base.OnEnable();

            backButton.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(usernameInputField.text))
                {
                    //TOdo make info text
                }
                else
                {
                    Session.SetSession(usernameInputField.text, 0);
                    ShowNextView();
                }
            });
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
    }

}
