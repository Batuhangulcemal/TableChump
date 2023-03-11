using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPlayingLAN = false;

    public static GameManager Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
       
    }

    public void OnLocalPlayerConnected()
    {
        UIManager.Instance.Show<LobbyView>();
    }
}
