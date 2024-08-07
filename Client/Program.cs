

using Client;

bool run = true;
GameContainer gc = new();
ConsoleKeyInfo? key = null;
while (run) {
    if (Console.KeyAvailable) {
        key = Console.ReadKey(true);
    }
    gc.Progress(key?.KeyChar);
    key = null;
    gc.Display();
}

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

