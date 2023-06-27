using System;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.Mechanic.GameCore
{
    public class ServerGameController : MonoBehaviour
    {
        public static ServerGameController Instance;

        private GameController controller;
        public static GameController Controller => GetController();

        private void Awake()
        {
            Instance = this;
            controller = new GameController();
        }

        private static GameController GetController()
        {
            if (!NetworkManager.Singleton.IsServer)
            {
                return Instance.controller;
            }

            Debug.Log("Client's can not access this component.");
            return null;
            
        }
    }
}