namespace Common.Packets;

public enum PacketType : byte {
    RequestJoin,
    RequestJoinAck,
    WaitingGame,
    GameStart,
    MyState,
    GameState,
    Bye
}