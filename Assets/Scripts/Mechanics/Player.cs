using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public FixedString32Bytes UserName = new ();
    public int[] Cards;
    public int Health = new();
    public bool IsAuthorized;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsLocalPlayer)
        {
            GameManager.Instance.OnLocalPlayerConnected();
        }
    }


    private void OnUserNameValueChanged(FixedString32Bytes previous, FixedString32Bytes current)
    {
        Debug.Log($"Detected NetworkVariable Change: Previous: {previous} | Current: {current}");
    }
}
