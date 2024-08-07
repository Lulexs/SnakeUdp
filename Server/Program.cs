using System.Net;
using System.Net.Sockets;
using System.Text;

UdpClient listener = new(11000);


while (true) {
    IPEndPoint cliEndpoint = new(IPAddress.Any, 11000);

    byte[] recvBytes = listener.Receive(ref cliEndpoint);
    Console.WriteLine(Encoding.UTF8.GetString(recvBytes));

    listener.Send(recvBytes, recvBytes.Length, cliEndpoint);
}
