using System;
using Unity.Collections;
using Unity.Netcode;


public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
{
    public FixedString32Bytes name;
    public ulong clientId;
    public int colorId;

    public bool Equals(PlayerData other)
    {
        return clientId == other.clientId && colorId == other.colorId && name == other.name;
    }


    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref name);
        serializer.SerializeValue(ref clientId);
        serializer.SerializeValue(ref colorId);
    }
}
