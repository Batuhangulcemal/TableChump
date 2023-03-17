using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetworkSelectionView : View
{
    [SerializeField] private Button join;
    [SerializeField] private Button create;
    [SerializeField] private Button server;
    public override void Initialize()
    {

        join.interactable = LocalGameManager.Instance.isPlayingLAN;

        join.onClick.AddListener(() => Join());
        create.onClick.AddListener(() => Create());
        server.onClick.AddListener(() => Server());

        base.Initialize();
    }

    private void Join()
    {
        NetworkManager.Singleton.StartClient();
    }
    private void Create()
    {
        NetworkManager.Singleton.StartHost();
    }
    private void Server()
    {
        NetworkManager.Singleton.StartServer();
    }
}
