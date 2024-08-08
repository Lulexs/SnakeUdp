using System.Net;
using System.Net.Sockets;
using System.Text;
using Common.Packets;

UdpClient listener = new(11000);


while (true) {
    IPEndPoint cliEndpoint = new(IPAddress.Any, 11000);

    byte[] recvBytes = listener.Receive(ref cliEndpoint);
    Packet packet = PacketFactory.FromBytes(recvBytes);
    Console.WriteLine(packet.PacketType.ToString());

    // listener.Send(recvBytes, recvBytes.Length, cliEndpoint);
}
