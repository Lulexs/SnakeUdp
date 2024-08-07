namespace Common.Packets;

public class UnknownPacketTypeException : ApplicationException {
    public UnknownPacketTypeException() : base() {}
    public UnknownPacketTypeException(string message) : base(message) {}
    public UnknownPacketTypeException(string message, Exception? inner) : base(message, inner) {}
}