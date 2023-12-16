using System;
using Unity.Netcode;

namespace AsepStudios.TableChump.Mechanics.PlayerCore
{
    public struct PlayerData : INetworkSerializable, IEquatable<PlayerData>
    {
        public NetworkObjectReference NetworkObject;
        public ulong ClientId;

        public Player Player => GetPlayer();

        private Player GetPlayer()
        {
            if (NetworkObject.TryGet(out NetworkObject networkObject))
            {
                return networkObject.GetComponent<Player>();
            }

            return null;
        }

        public bool Equals(PlayerData other)
        {
            return ClientId == other.ClientId;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref NetworkObject);
            serializer.SerializeValue(ref ClientId);
        }
    }
}
