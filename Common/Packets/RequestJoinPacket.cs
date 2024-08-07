namespace Common.Packets;

public class RequestJoinPacket : Packet {
    public RequestJoinPacket() : base(PacketType.RequestJoin) {}
}