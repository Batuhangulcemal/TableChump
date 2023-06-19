using AsepStudios.SceneManagement;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class ConnectionService
{
    public static void ConnectAsClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    public static void ConnectAsHost()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene(UnityScene.GameScene.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public static void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
        Loader.Load(UnityScene.MainMenuScene);
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

    private static void SetConnectionData(string ipAddress, ushort port = 7777)
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipAddress;
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port = port;
    }
}
