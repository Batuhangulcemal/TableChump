using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.TableChump.Mechanics.GameCore.Controller
{
    public class ServerGameController : MonoBehaviour
    {
        private static ServerGameController _instance;

        private GameController _controller;
        public static GameController Controller
        {
            get
            {
                if (NetworkManager.Singleton.IsServer)
                {
                    return _instance._controller ??= new GameController();
                }

                Debug.Log("Client's can not access this component.");
                return null;
            }
        }

        private void Awake()
        {
            _instance = this;
        }
        
    }
}