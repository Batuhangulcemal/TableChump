using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkGameManager : MonoBehaviour
{
    public static NetworkGameManager Instance { get; private set; }

    public event EventHandler OnLocalPlayerSpawned;

    public enum ConnectionType
    {
        Null,
        Client,
        Host,
        Server
    }

    public ConnectionType connectionType;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void ConnectAsClient()
    {
        connectionType = ConnectionType.Client;
        NetworkManager.Singleton.StartClient();
        Loader.Load(Loader.Scene.LobbyScene);
    }

    //TO BE REMOVED
    public void ConnectAsHost()
    {
        connectionType = ConnectionType.Host;
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene(Loader.Scene.LobbyScene.ToString(), LoadSceneMode.Single);

    }

    //TO BE REMOVED
    public void TestConnectAsServer()
    {
        connectionType = ConnectionType.Server;
        NetworkManager.Singleton.StartServer();
        Loader.Load(Loader.Scene.LobbyScene);

    }


    public void StopConnection()
    {
        NetworkManager.Singleton.Shutdown();
        connectionType = ConnectionType.Null;
        Loader.Load(Loader.Scene.MainMenuScene);
    }


    public void LocalPlayerSpawnedSignal()
    {
        OnLocalPlayerSpawned?.Invoke(this, EventArgs.Empty);
    }


}
