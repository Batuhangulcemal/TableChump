using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameManager : MonoBehaviour
{
    public static LocalGameManager Instance { get; private set; }

    [Header("Game Settings")]
    public bool isPlayingLAN = true;

    [Header("Profile Attributes")]
    public string userName;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void OnLocalPlayerConnected()
    {
        UIManager.Instance.Show<LobbyView>();
    }
}
