using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance {  get; private set; }

    public NetworkVariable<int> AuthorizedPlayerClientId = new (-1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        PlayerManager.Instance.OnPlayerListChanged += OnPlayerListChanged;
    }

    private void OnPlayerListChanged(Player player, PlayerOperation op)
    {
        if (op == PlayerOperation.Added)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                Debug.Log("server");
                if(AuthorizedPlayerClientId.Value == -1)
                {
                    Debug.Log("if");
                    AuthorizedPlayerClientId.Value = (int)player.OwnerClientId;
                }
            }
            Debug.Log(player + "Added");

        }
        else
        {
            if(NetworkManager.Singleton.IsServer)
            {
                if(AuthorizedPlayerClientId.Value == (int)player.OwnerClientId)
                {
                    if(PlayerManager.Instance.Players.Count == 0)
                    {
                        AuthorizedPlayerClientId.Value = -1;
                    }
                    else
                    {
                        AuthorizedPlayerClientId.Value = (int)PlayerManager.Instance.Players[0].OwnerClientId;
                    }
                }
            }
            Debug.Log(player + "Removed");

        }
    }
}
