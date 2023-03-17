using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public NetworkVariable<FixedString32Bytes> UserName;
    public NetworkList<int> Cards;
    public NetworkVariable<int> Health;

    private void Awake()
    {
        UserName = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        Cards = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        Health = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsLocalPlayer)
        {
            LocalGameManager.Instance.OnLocalPlayerConnected();
            UserName.Value = LocalGameManager.Instance.userName;
        }

        PlayerManager.Instance.AddPlayer(this);

    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        PlayerManager.Instance.RemovePlayer(this);

    }


    public override string ToString()
    {
        return UserName.Value.ToString();
    }
}
