using AsepStudios.SceneManagement;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public static class ConnectionService
{
    public static void Disconnect()
    {
        Loader.Load(UnityScene.MainMenuScene);
        NetworkManager.Singleton.Shutdown();
    }

    public static void ConnectAsClient(string ipAddress, ushort port = 7777)
    {
        SetConnectionData(ipAddress, port);
        ConnectAsClient();
    }

    public static void ConnectAsHost(string ipAddress, ushort port = 7777)
    {
        SetConnectionData(ipAddress, port);
        ConnectAsHost();
    }

    public static void ConnectAsClientLocally()
    {
        SetConnectionData("127.0.0.1", 7777);
        ConnectAsClient();
    }
    public static void ConnectAsHostLocally()
    {
        SetConnectionData("127.0.0.1", 7777);
        ConnectAsHost();
    }
    
    private static void ConnectAsClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    private static void ConnectAsHost()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene(UnityScene.GameScene.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    private static void SetConnectionData(string ipAddress, ushort port = 7777)
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipAddress;
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port = port;
    }
}
