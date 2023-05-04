using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    public static LobbyManager Instance { get; private set; }

    public event EventHandler OnPlayerDataNetworkListChanged;

    private NetworkList<PlayerData> playerDataNetworkList;


    private void Awake()
    {
        Instance = this;

        playerDataNetworkList = new();
        playerDataNetworkList.OnListChanged += PlayerDataNetworkList_OnListChanged;

    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer)
        {
            NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_Server_ConnectionApprovalCallback;
            NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_Server_OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_Server_OnClientDisconnectCallback;
        }
    }


    private void NetworkManager_Server_ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest connectionApprovalRequest, NetworkManager.ConnectionApprovalResponse connectionApprovalResponse)
    {
        Debug.Log("Connection approval");
        connectionApprovalResponse.Approved = true;

    }

    private void NetworkManager_Server_OnClientConnectedCallback(ulong clientId)
    {
        Debug.Log("Server: player connected");
        playerDataNetworkList.Add(new PlayerData
        {
            clientId = clientId,
            colorId = (int)clientId,
            name = clientId.ToString()
        });
    }

    private void NetworkManager_Server_OnClientDisconnectCallback(ulong clientId)
    {
        Debug.Log("Server: player disconnected");
        for (int i = 0; i < playerDataNetworkList.Count; i++)
        {
            PlayerData playerData = playerDataNetworkList[i];
            if (playerData.clientId == clientId)
            {
                //disconnected
                playerDataNetworkList.RemoveAt(i);
            }
        }
    }

    private void PlayerDataNetworkList_OnListChanged(NetworkListEvent<PlayerData> changeEvent)
    {
        Debug.Log("Player List Changed");
        OnPlayerDataNetworkListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<PlayerData> GetPlayerDataList()
    {
        List<PlayerData> result = new();
        foreach(var playerData in playerDataNetworkList)
        {
            result.Add(playerData);
        }

        return result;
    }

}
