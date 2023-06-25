using System;
using AsepStudios.Utils;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class JoinGameView : View
    {
        [SerializeField] private Button joinButton;
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI logText;
        [SerializeField] private GameObject loadingIcon;

        [SerializeField] private TMP_InputField sessionCodeInputField; //To be activated
        protected override void OnEnable()
        {
            base.OnEnable();
            
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnected;
            logText.text = string.Empty;
            loadingIcon.SetActive(false);
            
            joinButton.onClick.AddListener(() =>
            {
                logText.text = string.Empty;
                loadingIcon.SetActive(true);
                ConnectionService.ConnectAsClientLocally();
            });

            backButton.onClick.AddListener(() =>
            {
                ConnectionService.Disconnect();
                ViewManager.ShowView<SelectJoinOrHostView>();
            });
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            
            NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnected;
        }
        
        private void NetworkManager_OnClientDisconnected(ulong obj)
        {
            if (NetworkManager.Singleton.DisconnectReason != string.Empty)
            {
                logText.text = NetworkManager.Singleton.DisconnectReason;
            }
            else
            {
                logText.text = ErrorMessage.ConnectionError;
            }
            
            loadingIcon.SetActive(false);

        }
    }

}
