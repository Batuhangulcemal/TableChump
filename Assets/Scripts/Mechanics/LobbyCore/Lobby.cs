using AsepStudios.Mechanic.PlayerCore;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.Mechanic.Lobby
{
    public class Lobby : NetworkBehaviour
    {
        public static Lobby Instance { get; private set; }

        public event EventHandler OnPlayerListChanged;
        public bool IsHostPlayerActive => NetworkManager.Singleton.IsHost;
        public bool IsAllReady => GetIsAllReady();



        private NetworkList<NetworkObjectReference> players;

        [SerializeField] private NetworkObject playerPrefab;

        private void Awake()
        {
            Instance = this;
            players = new();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsServer)
            {
                NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
                NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

                if (IsHost)
                {
                    SpawnPlayerObject(NetworkManager.Singleton.LocalClientId);
                }
            }

            NetworkManager.Singleton.OnClientDisconnectCallback += Client_NetworkManager_OnClientDisconnectCallback;

            players.OnListChanged += Players_OnListChanged;
        }

        public List<Player> GetPlayers()
        {

            List<Player> playerList = new();

            foreach(NetworkObject playerNO in players)
            {
                playerList.Add(playerNO.GetComponent<Player>());
            }

            return playerList;
        }

        private bool GetIsAllReady()
        {
            foreach(Player player in GetPlayers())
            {
                if (!player.GetReady())
                {
                    return false;
                }
            }

            return true;
        }

        private void Players_OnListChanged(NetworkListEvent<NetworkObjectReference> changeEvent)
        {
            OnPlayerListChanged.Invoke(this, EventArgs.Empty);
        }

        private void NetworkManager_OnClientConnectedCallback(ulong clientId)
        {
            SpawnPlayerObject(clientId);
        }

        private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
        {
            RemovePlayerObject(clientId);
        }

        private void Client_NetworkManager_OnClientDisconnectCallback(ulong clientId)
        {
            if (NetworkManager.Singleton.IsServer) return;

            ConnectionService.Disconnect();
        }

        private NetworkObject SpawnPlayerObject(ulong clientId)
        {
            NetworkObject playerNetworkObject = Instantiate(playerPrefab);
            playerNetworkObject.SpawnAsPlayerObject(clientId, true);
            players.Add(playerNetworkObject);
            return playerNetworkObject;
        }

        private void RemovePlayerObject(ulong clientId)
        {
            players.Remove(NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject);
        }


    }
}
