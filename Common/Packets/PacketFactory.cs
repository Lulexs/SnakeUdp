namespace Common.Packets;

public static class PacketFactory {
    public static Packet FromBytes(byte[] bytes) {
        PacketType type = (PacketType)bytes[0];

        return type switch
        {
            PacketType.RequestJoin => new RequestJoinPacket(),
            PacketType.RequestJoinAck => new RequestJoinAckPacket(),
            PacketType.WaitingGame => new WaitingGamePacket(),
            PacketType.GameStart => new GameStartPacket(bytes.Skip(1).ToArray()),
            PacketType.MyState => new MyStatePacket(bytes.Skip(1).ToArray()),
            PacketType.Bye => new ByePacket(bytes.Skip(1).ToArray()),
            _ => throw new UnknownPacketTypeException(),
        };
    }
}