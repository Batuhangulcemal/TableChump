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
    public override void Initialize()
    {

        join.interactable = GameManager.Instance.isPlayingLAN;

        join.onClick.AddListener(() => Join());
        create.onClick.AddListener(() => Create());

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
}
