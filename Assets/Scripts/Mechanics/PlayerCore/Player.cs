using AsepStudios.App;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;
using System;
using AsepStudios.Mechanic.LobbyCore;
using Unity.Collections;
using Unity.Netcode;

namespace AsepStudios.Mechanic.PlayerCore
{
    public class Player : NetworkBehaviour
    {
        public event EventHandler OnAnyPlayerPropertyChanged;

        private readonly NetworkVariable<FixedString32Bytes> username = new("",
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        
        private readonly NetworkVariable<int> avatarIndex = new(0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private readonly NetworkVariable<bool> ready = new(false,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        
        
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

        private void AvatarIndexOnValueChanged(int previousvalue, int newvalue)
        {
            OnAnyPlayerPropertyChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ReadyOnValueChanged(bool previousValue, bool newValue)
        {
            OnAnyPlayerPropertyChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UsernameOnValueChanged(FixedString32Bytes previousValue, FixedString32Bytes newValue)
        {
            OnAnyPlayerPropertyChanged?.Invoke(this, EventArgs.Empty);
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
        internal void SetReady(bool newReady)
        {
            ready.Value = newReady;
        }
    }

}

