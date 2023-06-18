using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class JoinGameView : View
    {
        [SerializeField] private Button joinButton;
        [SerializeField] private Button backButton;

        [SerializeField] private TMP_InputField sessionCodeInputField; //To be activated
        protected override void OnEnable()
        {
            base.OnEnable();

            joinButton.onClick.AddListener(() =>
            {
                ConnectionService.ConnectAsClientLocally();
            });

            backButton.onClick.AddListener(() =>
            {
                ViewManager.ShowView<SelectJoinOrHostView>();
            });
        }
    }

}
