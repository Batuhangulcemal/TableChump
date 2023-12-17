using AsepStudios.TableChump.Utils;
using AsepStudios.TableChump.Utils.Service;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace AsepStudios.TableChump.UI.Views.MainMenuScene
{
    public class JoinGameWithIpView : View
    {
        [SerializeField] private Button joinButton;
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI logText;
        [SerializeField] private GameObject loadingIcon;

        [SerializeField] private TMP_InputField ipInputField; //To be activated
        [SerializeField] private TMP_InputField portInputField; //To be activated
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnected;
            logText.text = string.Empty;
            loadingIcon.SetActive(false);
            
            joinButton.onClick.AddListener(TryConnectAsHost);

            backButton.onClick.AddListener(() =>
            {
                ConnectionService.Disconnect();
                ViewManager.ShowView<JoinGameView>();
            });
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnected;
            }
        }

        private void TryConnectAsHost()
        {
            logText.text = string.Empty;

            if (!CheckInputFieldsFulfilled())
            {
                logText.text = ErrorMessage.PleaseFillRequiredFields;
                return;
            }
            
            loadingIcon.SetActive(true);
            var ip = ipInputField.text;
            var port = ushort.Parse(portInputField.text);
            ConnectionService.ConnectAsClient(ip, port);
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

        private bool CheckInputFieldsFulfilled()
        {
            return !string.IsNullOrEmpty(ipInputField.text) && !string.IsNullOrEmpty(portInputField.text);
        }
    }

}
