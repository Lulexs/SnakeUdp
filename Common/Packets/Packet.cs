namespace Common.Packets;

public class Packet {
    public byte[] Payload { get; set; } = [];
    public PacketType PacketType { get; set; }

    public Packet() {

    }

    public Packet(PacketType packetType) {
        PacketType = packetType;
    }
}

