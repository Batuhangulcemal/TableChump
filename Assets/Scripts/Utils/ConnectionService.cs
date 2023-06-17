using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class ConnectionService
{
    public static void ConnectAsClient(string ipAddress, ushort port = 7777)
    {
        SetConnectionData(ipAddress, port);
        NetworkManager.Singleton.StartClient();
    }

    public static void ConnectAsHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    private static void SetConnectionData(string ipAddress, ushort port = 7777)
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipAddress;
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port = port;
    }
}
