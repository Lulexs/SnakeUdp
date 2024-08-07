
// using System.Net;
// using System.Net.Sockets;
// using System.Text;

// UdpClient client = new();
// IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Loopback, 11000);

// while (true) {
//     string msg = Console.ReadLine()!;

//     byte[] bytesToSend = Encoding.UTF8.GetBytes(msg);

//     client.Send(bytesToSend, bytesToSend.Length, serverEndpoint);

//     byte[] recievedBytes = client.Receive(ref serverEndpoint);
//     string recievedMsg = Encoding.UTF8.GetString(recievedBytes);

//     Console.WriteLine(recievedMsg);
// }

