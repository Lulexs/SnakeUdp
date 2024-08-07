namespace Common.Packets;

public class GameStartPacket : Packet {
    public GameStartPacket() : base(PacketType.GameStart) {}
}