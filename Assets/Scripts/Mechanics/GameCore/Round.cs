using System;
using AsepStudios.Mechanic.GameCore.Enum;
using Unity.Netcode;

namespace AsepStudios.Mechanic.GameCore
{
    public class Round : NetworkBehaviour
    {
        public event EventHandler OnRoundStateChanged;
        public event EventHandler OnRowChoosePlayerChanged;
        
        public static Round Instance { get; private set; }
        
        private readonly NetworkVariable<RoundState> roundState = new();
        private readonly NetworkVariable<int> rowChoosePlayer = new(-1);

        public RoundState RoundState => roundState.Value;
        public int RowChoosePlayer => rowChoosePlayer.Value;

        private void Awake()
        {
            Instance = this;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            roundState.OnValueChanged += RoundState_OnValueChanged;
            rowChoosePlayer.OnValueChanged += RowChoosePlayer_OnStatePlayer;
        }

        public void ChangeRoundState(RoundState roundState)
        {
            this.roundState.Value = roundState;
        }

        public void ChangeRowChoosePlayer(int playerId)
        {
            rowChoosePlayer.Value = playerId;
        }
        
        private void RoundState_OnValueChanged(RoundState previousValue, RoundState newValue)
        {
            OnRoundStateChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private void RowChoosePlayer_OnStatePlayer(int previousValue, int newValue)
        {
            OnRowChoosePlayerChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}