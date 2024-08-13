
namespace Common.Packets;

public class MyStatePacket : Packet {
    public int FoodI { 
        get { return BitConverter.ToInt32(Payload, 0); }
        set { BitConverter.GetBytes(value).CopyTo(Payload, 0); } 
    }
    public int FoodJ { 
        get { return BitConverter.ToInt32(Payload, 4); }
        set { BitConverter.GetBytes(value).CopyTo(Payload, 4); } 
    }
    public int UndisplayI { 
        get { return BitConverter.ToInt32(Payload, 8); }
        set { BitConverter.GetBytes(value).CopyTo(Payload, 8); } 
    }
    public int UndisplayJ { 
        get { return BitConverter.ToInt32(Payload, 12); }
        set { BitConverter.GetBytes(value).CopyTo(Payload, 12); } 
    }
    public int DisplayI { 
        get { return BitConverter.ToInt32(Payload, 16); }
        set { BitConverter.GetBytes(value).CopyTo(Payload, 16); } 
    }
    public int DisplayJ { 
        get { return BitConverter.ToInt32(Payload, 20); }
        set { BitConverter.GetBytes(value).CopyTo(Payload, 20); } 
    }
    public int Tag{ 
        get { return BitConverter.ToInt32(Payload, 24); }
        set { BitConverter.GetBytes(value).CopyTo(Payload, 24); } 
    }
    public static int Count { get; set; }

    public MyStatePacket() : base(PacketType.MyState) {
        Count += 1;
    }

    public MyStatePacket(Tuple<int, int, int, int, int, int> state) : base(PacketType.MyState) {
        Payload = new byte[7 * sizeof(int)];
        FoodI = state.Item1;
        FoodJ = state.Item2;
        UndisplayI = state.Item3;
        UndisplayJ = state.Item4;
        DisplayI = state.Item5;
        DisplayJ = state.Item6;
        Tag = Count;
        Count += 1;
    }

    public MyStatePacket(byte[] payload) : base(PacketType.MyState) {
        Payload = payload;
    }
}