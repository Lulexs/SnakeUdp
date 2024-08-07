
namespace Common.Packets;

public class Packet {
    public byte[] Payload { get; set; } = [];
    public PacketType PacketType { get; set; }

    public Packet() {

    }

    public Packet(PacketType packetType) {
        PacketType = packetType;
    }

    public byte[] GetBytes() {
        byte[] bytes = new byte[Payload.Length + sizeof(PacketType)];

        BitConverter.GetBytes((int)PacketType).CopyTo(bytes, 0);
        Payload.CopyTo(Payload, sizeof(PacketType));

        return bytes;
    }
}

