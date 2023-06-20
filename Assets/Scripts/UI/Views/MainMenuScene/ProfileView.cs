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
                    ViewManager.ShowView<MainMenuView>();
                }
            });

            
        }
    }

}
