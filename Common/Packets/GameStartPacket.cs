namespace Common.Packets;

public class GameStartPacket : Packet {
    public int Seed {
        get { return BitConverter.ToInt32(Payload); } 
        set { Payload = BitConverter.GetBytes(value); } 
    }
    public GameStartPacket() : base(PacketType.GameStart) {}

    public GameStartPacket(int seed) : base(PacketType.GameStart) {
        Seed = seed;
    }

    public GameStartPacket(byte[] payload) : base(PacketType.GameStart) {
        Payload = payload;
    }
}