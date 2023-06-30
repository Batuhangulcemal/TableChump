using System;
using Unity.Netcode;

namespace AsepStudios.Mechanic.GameCore
{
    public struct GameArgs : INetworkSerializable, IEquatable<GameArgs>
    {
        public bool IsArgsInitialized;
        public int PlayerPointStartValue;
        public int MaxPlayerCount;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref IsArgsInitialized);
            serializer.SerializeValue(ref PlayerPointStartValue);
            serializer.SerializeValue(ref MaxPlayerCount);
        }

        public bool Equals(GameArgs other)
        {
            return IsArgsInitialized == other.IsArgsInitialized &&
                   PlayerPointStartValue == other.PlayerPointStartValue &&
                   MaxPlayerCount == other.MaxPlayerCount;
        }
    }
}