using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkGameManager : NetworkBehaviour
{
    public static NetworkGameManager Instance { get; private set; }

    public event EventHandler OnLocalPlayerSpawned;
    public event EventHandler OnConnect;
    public event EventHandler OnDisconnect;

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

    private void Start()
    {
        //NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        //NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
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
        NetworkManager.Singleton.SceneManager.LoadScene(Loader.Scene.LobbyScene.ToString(), LoadSceneMode.Single);

    }

    public void StopConnection()
    {
        NetworkManager.Singleton.Shutdown();
        connectionType = ConnectionType.Null;

    }


    public void LocalPlayerSpawnedSignal()
    {
        OnLocalPlayerSpawned?.Invoke(this, EventArgs.Empty);
    }


}
