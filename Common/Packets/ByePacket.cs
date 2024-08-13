namespace Common.Packets;

public class ByePacket : Packet {
    public int Score {
        get { return BitConverter.ToInt32(Payload); } 
        set { Payload = BitConverter.GetBytes(value); } 
    }
    public ByePacket() : base(PacketType.Bye) {}

    public ByePacket(int score) : base(PacketType.GameStart) {
        Score = score;
    }

    public ByePacket(byte[] payload) : base(PacketType.GameStart) {
        Payload = payload;
    }
}