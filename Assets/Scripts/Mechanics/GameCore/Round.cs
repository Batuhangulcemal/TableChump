using AsepStudios.Mechanic.GameCore.Enum;
using Unity.Netcode;
using UnityEngine;

namespace AsepStudios.Mechanic.GameCore
{
    public class Round : NetworkBehaviour
    {
        public readonly NetworkVariable<RoundState> RoundState = new();
        
        [SerializeField] private Board board;
        public Board Board => board;
    }
}