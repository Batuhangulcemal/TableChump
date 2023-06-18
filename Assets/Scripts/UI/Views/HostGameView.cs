using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class HostGameView : View
    {
        [SerializeField] private Button hostButton;
        [SerializeField] private Button backButton;

        protected override void OnEnable()
        {
            base.OnEnable();

            hostButton.onClick.AddListener(() =>
            {
                ConnectionService.ConnectAsHostLocally();
            });

            backButton.onClick.AddListener(() =>
            {
                ViewManager.ShowView<SelectJoinOrHostView>();
            });
        }
    }

}

