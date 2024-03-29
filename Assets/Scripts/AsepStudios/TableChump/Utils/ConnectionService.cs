using System;
using AsepStudios.TableChump.Mechanics.GameCore;
using AsepStudios.TableChump.Mechanics.GameCore.Enum;
using AsepStudios.TableChump.Mechanics.LobbyCore;
using AsepStudios.TableChump.Utils.SceneManagement;
using AsepStudios.TableChump.Utils.Service;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AsepStudios.TableChump.Utils
{
    public static class ConnectionService
    {
        public static void Disconnect()
        {
            if (SceneManager.GetActiveScene().name != UnityScene.MainMenuScene.ToString())
            {
                Loader.Load(UnityScene.MainMenuScene);
            }
            NetworkManager.Singleton.Shutdown();
        }

        public static void ConnectAsClient(string ipAddress, ushort port = 7777)
        {
            SetConnectionData(ipAddress, port);
            ConnectAsClient();
        }

        public static void ConnectAsHost(string ipAddress, ushort port = 7777)
        {
            SetConnectionData(ipAddress, port);
            ConnectAsHost();
        }

        public static void ConnectAsClientLocally()
        {
            SetConnectionData("127.0.0.1", 7777);
            ConnectAsClient();
        }
        public static void ConnectAsHostLocally()
        {
            SetConnectionData("127.0.0.1", 7777);
            ConnectAsHost();
        }
    
        private static void ConnectAsClient()
        {
            try
            {
                NetworkManager.Singleton.StartClient();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        
        }

        private static void ConnectAsHost()
        {
            NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.SceneManager.LoadScene(UnityScene.GameScene.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    
        private static void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            if (NetworkManager.Singleton.LocalClientId == request.ClientNetworkId)
            {
                //Host player, always approve
                response.Approved = true;
                return;

            }
            if (Game.Instance == null)
            {
                response.Reason = ErrorMessage.RoomIsBeingCreated;
                response.Approved = false;
                return;
            }
        
            if (!Game.Instance.IsArgsInitialized)
            {
                response.Reason = ErrorMessage.RoomIsBeingCreated;
                response.Approved = false;
                return;
            }

            if (Game.Instance.GameState.Value != GameState.NotStarted)
            {
                response.Reason = ErrorMessage.GameAlreadyStarted;
                response.Approved = false;
                return;
            }
        
            if (Lobby.Instance.PlayerCount >= Game.Instance.MaxPlayerCount) 
            {
                response.Reason = ErrorMessage.RoomIsFull;
                response.Approved = false;
                return;
            }

            response.Approved = true;
        }



        private static void SetConnectionData(string ipAddress, ushort port = 7777)
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipAddress;
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port = port;
        }
    

    }
}
