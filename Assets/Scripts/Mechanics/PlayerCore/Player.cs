using AsepStudios.App;
using AsepStudios.Mechanic.PlayerCore.LocalPlayerCore;
using System;
using Unity.Collections;
using Unity.Netcode;

namespace AsepStudios.Mechanic.PlayerCore
{
    public class Player : NetworkBehaviour
    {
        public event EventHandler OnAnyPlayerPropertyChanged;

        private NetworkVariable<FixedString32Bytes> username = new("",
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                LocalPlayer.Instance.AttachPlayer(this);
                username.Value = Session.Username;
            }

            username.OnValueChanged += UsernameOnValueChanged;
        }

        private void UsernameOnValueChanged(FixedString32Bytes previousValue, FixedString32Bytes newValue)
        {
            OnAnyPlayerPropertyChanged?.Invoke(this, EventArgs.Empty);
        }

        public string GetUsername()
        {
            return username.Value.ToString();
        }

        internal void SetUserName(string newUsername)
        {
            username.Value = newUsername;
        }
    }

}

