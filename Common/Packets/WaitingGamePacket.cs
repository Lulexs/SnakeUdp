namespace Common.Packets;

public class WaitingGamePacket : Packet {
    public WaitingGamePacket() : base(PacketType.WaitingGame) {}
}