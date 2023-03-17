using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System.Linq;
using System;


public enum PlayerOperation
{
    Added,
    Removed
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance {  get; private set; }

    public List<Player> Players;

    public event Action<Player, PlayerOperation> OnPlayerListChanged; 

    private void Awake()
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

    public void AddPlayer(Player player)
    {
        Players.Add(player);
        OnPlayerListChanged.Invoke(player, PlayerOperation.Added);
    }

    public void RemovePlayer(Player player)
    {
        Players.Remove(player);
        OnPlayerListChanged.Invoke(player, PlayerOperation.Removed);
    }

    public Player GetPlayerFromClientId(ulong clientID)
    {
        return (from p in Players where p.OwnerClientId == clientID select p).First();
    }
}
