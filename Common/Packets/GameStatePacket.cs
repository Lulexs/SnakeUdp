namespace Common.Packets;

public class GameStatePacket : Packet {
    public GameStatePacket() : base(PacketType.GameState) {}
}