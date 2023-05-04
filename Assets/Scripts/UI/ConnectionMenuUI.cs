using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionMenuUI : UI
{
    [SerializeField] private Button joinButton;
    [SerializeField] private Button createButton;


    public override void Initialize()
    {
        base.Initialize();

        joinButton.onClick.AddListener(() =>
        {
            NetworkGameManager.Instance.ConnectAsClient();
        });

        createButton.onClick.AddListener(() =>
        {
            NetworkGameManager.Instance.ConnectAsHost();
        });


    }
}
