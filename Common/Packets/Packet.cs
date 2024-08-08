
namespace Common.Packets;

public class Packet {
    public byte[] Payload { get; set; } = [];
    public PacketType PacketType { get; set; }

    public Packet() {

    }

    public Packet(PacketType packetType) {
        PacketType = packetType;
    }

    public virtual byte[] GetBytes() {
        byte[] bytes = new byte[Payload.Length + sizeof(PacketType)];

        bytes[0] = (byte)PacketType;
        Payload.CopyTo(bytes, sizeof(PacketType));

        return bytes;
    }
}

