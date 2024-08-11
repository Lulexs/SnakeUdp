namespace Common.Packets;

public static class PacketFactory {
    public static Packet FromBytes(byte[] bytes) {
        PacketType type = (PacketType)bytes[0];

        return type switch
        {
            PacketType.RequestJoin => new RequestJoinPacket(),
            PacketType.RequestJoinAck => new RequestJoinAckPacket(),
            PacketType.WaitingGame => new WaitingGamePacket(),
            PacketType.GameStart => new GameStartPacket(),
            PacketType.MyState => new MyStatePacket(),
            PacketType.GameState => new GameStatePacket(),
            PacketType.Bye => new ByePacket(),
            _ => throw new UnknownPacketTypeException(),
        };
    }
}