using AsepStudios.API;
using AsepStudios.API.Dto;
using AsepStudios.API.Service;
using AsepStudios.Mechanic.LobbyCore;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;
using UnityEngine;

public class Test : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(Lobby.Instance.GetPlayers().Count);
        }
        
    }
}
