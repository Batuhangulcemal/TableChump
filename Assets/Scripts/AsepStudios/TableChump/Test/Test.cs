using System.Collections.Generic;
using AsepStudios.API;
using AsepStudios.API.Dto;
using AsepStudios.API.Service;
using AsepStudios.Mechanic.GameCore;
using AsepStudios.Mechanic.LobbyCore;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;
using UnityEngine;

public class Test : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CardDealer cardDealer = new CardDealer();
        }
        
    }
}
