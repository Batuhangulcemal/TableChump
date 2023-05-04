using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : UI
{

    [SerializeField] private Button startButton;
    [SerializeField] private Transform playerListViewContentTransform;
    [SerializeField] private Transform playerLobbyViewGameObject;

    public override void Initialize()
    {
        base.Initialize();

        startButton.onClick.AddListener(() =>
        {
            Debug.Log("Start");
            NetworkManager.Singleton.SceneManager.LoadScene(Loader.Scene.GameScene.ToString(), LoadSceneMode.Single);
        });
    }

    private void Start()
    {
        LobbyManager.Instance.OnPlayerDataNetworkListChanged += LobbyManager_OnPlayerDataNetworkListChanged;
        RefreshPlayerList();
    }

    private void LobbyManager_OnPlayerDataNetworkListChanged(object sender, System.EventArgs e)
    {
        RefreshPlayerList();
    }

    private void RefreshPlayerList()
    {
        foreach (Transform child in playerListViewContentTransform)
        {
            Destroy(child.gameObject);
        }

        foreach(PlayerData playerData in LobbyManager.Instance.GetPlayerDataList())
        {
            Transform playerView = Instantiate(playerLobbyViewGameObject, playerListViewContentTransform);
            playerView.name = playerData.clientId.ToString();
            playerView.gameObject.SetActive(true);
        }
    }
}
