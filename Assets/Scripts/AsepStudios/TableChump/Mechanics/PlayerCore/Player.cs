using System;
using AsepStudios.TableChump.App;
using AsepStudios.TableChump.Mechanics.LobbyCore;
using AsepStudios.TableChump.Mechanics.PlayerCore.LocalPlayerCore;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.TableChump.Mechanics.PlayerCore
{
    [RequireComponent(typeof(GamePlayer))]
    public class Player : NetworkBehaviour
    {
        public event Action OnAnyPlayerPropertyChanged;
        
        public GamePlayer GamePlayer => GetGamePlayer();
        private GamePlayer gamePlayer;

        private NetworkVariable<FixedString32Bytes> username;

        private NetworkVariable<int> avatarIndex;

        private NetworkVariable<bool> ready;

        private void Awake()
        {
            username = new("",
                NetworkVariableReadPermission.Everyone,
                NetworkVariableWritePermission.Owner);
            
            avatarIndex = new(0,
                NetworkVariableReadPermission.Everyone,
                NetworkVariableWritePermission.Owner);
            
            ready = new(false,
                NetworkVariableReadPermission.Everyone,
                NetworkVariableWritePermission.Owner);

        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                LocalPlayer.Instance.AttachPlayer(this);
                username.Value = Session.Username;
                avatarIndex.Value = Session.AvatarIndex;

                if (IsServer)
                {
                    Lobby.Instance.SetLobbyName($"{Session.Username}'s Lobby");
                }
                
            }

            username.OnValueChanged += UsernameOnValueChanged;
            avatarIndex.OnValueChanged += AvatarIndexOnValueChanged;
            ready.OnValueChanged += ReadyOnValueChanged;
        }
        
        public string GetUsername()
        {
            return username.Value.ToString();
        }
        public int GetAvatarIndex()
        {
            return avatarIndex.Value;
        }
        public bool GetReady()
        {
            return ready.Value;
        }
        
        public void SetReadyLocal(bool newReady)
        {
            ready.Value = newReady;
        }

        private GamePlayer GetGamePlayer()
        {
            if (gamePlayer == null)
            {
                gamePlayer = GetComponent<GamePlayer>();
            }

            return gamePlayer;
        }
        
        private void AvatarIndexOnValueChanged(int previousvalue, int newvalue)
        {
            OnAnyPlayerPropertyChanged?.Invoke();
        }

        private void ReadyOnValueChanged(bool previousValue, bool newValue)
        {
            OnAnyPlayerPropertyChanged?.Invoke();
        }

        private void UsernameOnValueChanged(FixedString32Bytes previousValue, FixedString32Bytes newValue)
        {
            OnAnyPlayerPropertyChanged?.Invoke();
        }


 
    }

}

