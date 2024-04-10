using System;
using System.Collections.Generic;
using System.Linq;
using AsepStudios.TableChump.Mechanics.PlayerCore;
using AsepStudios.TableChump.Utils;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.TableChump.Mechanics.LobbyCore
{
    public class Lobby : NetworkBehaviour
    {
        public static Lobby Instance { get; private set; }
        public event Action OnPlayerListChanged;
        public event Action OnLobbyNameChanged;
        public bool IsHostPlayerActive => NetworkManager.Singleton.IsHost;
        public bool IsAllReady => GetIsAllReady();
        public List<Player> Players => GetPlayers();
        public int PlayerCount => players.Count;

        private readonly NetworkVariable<FixedString32Bytes> lobbyName = new("");
        private NetworkList<PlayerData> players;
        
        [SerializeField] private NetworkObject playerPrefab;
        
        private void Awake()
        {
            Instance = this;
            players = new NetworkList<PlayerData>();
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
            lobbyName.OnValueChanged += LobbyName_OnValueChanged;
        }
        
        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            
            if (IsServer)
            {
                NetworkManager.Singleton.OnClientConnectedCallback -= NetworkManager_OnClientConnectedCallback;
                NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnectCallback;
            }
            NetworkManager.Singleton.OnClientDisconnectCallback -= Client_NetworkManager_OnClientDisconnectCallback;
        }
        

        
        public Player GetPlayerFromClientId(ulong clientId)
        {
            foreach(var playerData in players)
            {
                if(playerData.ClientId == clientId)
                {
                    return playerData.Player;
                }
            }
            
            Debug.LogWarning("Player not found.");
            return null;
        }

        public string GetLobbyName()
        {
            return lobbyName.Value.ToString();
        }

        public void SetLobbyName(string lobbyName)
        {
            this.lobbyName.Value = lobbyName;
        }
        
        private List<Player> GetPlayers()
        {
            List<Player> playerList = new();

            foreach(var playerData in players)
            {
                playerList.Add(playerData.Player);
            }
            return playerList;
        }

        private bool GetIsAllReady()
        {
            return GetPlayers().All(player => player.GetReady());
        }

        private void Players_OnListChanged(NetworkListEvent<PlayerData> changeEvent)
        {
            OnPlayerListChanged?.Invoke();
        }
        private void LobbyName_OnValueChanged(FixedString32Bytes previousName, FixedString32Bytes newName)
        {
            OnLobbyNameChanged?.Invoke();
        }

        private void NetworkManager_OnClientConnectedCallback(ulong clientId)
        {
            if (clientId == NetworkManager.LocalClientId) return; 
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

        private void SpawnPlayerObject(ulong clientId)
        {
            NetworkObject playerNetworkObject = Instantiate(playerPrefab);
            playerNetworkObject.SpawnAsPlayerObject(clientId, true);
            players.Add(new PlayerData
            {
                NetworkObject = playerNetworkObject,
                ClientId = clientId
            });
        }

        private void RemovePlayerObject(ulong clientId)
        {
            for (var i = 0; i < players.Count; i++)
            {
                PlayerData playerData = players[i];
                if (playerData.ClientId == clientId)
                {
                    //disconnected
                    players.RemoveAt(i);
                }
            }
        }


    }
}
