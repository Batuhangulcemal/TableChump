using System;
using Unity.Netcode;
namespace AsepStudios.Mechanics.PlayerCore
{
    public struct PlayerData : INetworkSerializable, IEquatable<PlayerData>
    {
        public NetworkObjectReference Player;
        public ulong ClientId;

        public bool Equals(PlayerData other)
        {
            return ClientId == other.ClientId;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Player);
            serializer.SerializeValue(ref ClientId);
        }
    }
}
