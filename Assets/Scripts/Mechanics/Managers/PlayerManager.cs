using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public Player[] players;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback -= Singleton_OnClientDisconnectCallback;
    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        Debug.Log("asd");
        SendRefreshRpcToClients();
    }

    private void Singleton_OnClientDisconnectCallback(ulong obj)
    {
        Debug.Log("asd");
        SendRefreshRpcToClients();
    }

    private void SendRefreshRpcToClients()
    {
        if (!IsServer)
        {
            Debug.Log("How?");
            return;
        }

        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = NetworkManager.Singleton.ConnectedClientsIds
            }
        };

        RefreshPlayerListClientRpc(clientRpcParams);
    }



    [ClientRpc]
    public void RefreshPlayerListClientRpc(ClientRpcParams clientRpcParams = default)
    {
        players = FindObjectsOfType(typeof(Player)) as Player[];
    }

}
