using System;
using AsepStudios.Mechanic.GameCore.Enum;
using Unity.Netcode;
using Unity.VisualScripting;

namespace AsepStudios.Mechanic.GameCore
{
    public struct RoundInfo : INetworkSerializable, IEquatable<RoundInfo>
    {
        public RoundState RoundState;
        public int RowChoosePlayer;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref RoundState);
            serializer.SerializeValue(ref RowChoosePlayer);
        }
        public bool Equals(RoundInfo other)
        {
            return RoundState == other.RoundState && RowChoosePlayer == other.RowChoosePlayer;
        }
    }
    public class Round : NetworkBehaviour
    {
        public event EventHandler OnRoundInfoChanged;
        
        public static Round Instance { get; private set; }

        private readonly NetworkVariable<RoundInfo> roundInfo = new();

        public RoundState RoundState => roundInfo.Value.RoundState;
        public int RowChoosePlayer => roundInfo.Value.RowChoosePlayer;
        public RoundInfo RoundInfo => roundInfo.Value;
        private void Awake()
        {
            Instance = this;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            roundInfo.OnValueChanged += RoundInfo_OnValueChanged;
        }
        
        public void ChangeRoundState(RoundState roundState)
        {
            RoundInfo newRoundInfo = roundInfo.Value;
            newRoundInfo.RoundState = roundState;
            roundInfo.Value = newRoundInfo;
        }

        public void ChangeRowChoosePlayer(int playerId)
        {
            RoundInfo newRoundInfo = roundInfo.Value;
            newRoundInfo.RowChoosePlayer = playerId;
            roundInfo.Value = newRoundInfo;
        }

        private void RoundInfo_OnValueChanged(RoundInfo previousvalue, RoundInfo newvalue)
        {
            OnRoundInfoChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}