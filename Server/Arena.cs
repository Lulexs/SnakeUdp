using System.Net;

namespace Server;

class Arena(IPEndPoint player1, IPEndPoint player2) {
    public IPEndPoint Player1 { get; set; } = player1;
    public IPEndPoint Player2 { get; set; } = player2;

    
}