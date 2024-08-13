using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using Common.Packets;
using Server;

UdpClient listener = new(11000);

IPEndPoint? _playerInQueue = null;
ConcurrentDictionary<string, Arena> _playersToArenas = new();

ConcurrentQueue<Tuple<Packet, IPEndPoint>> _outgoingPackets = new();
ConcurrentQueue<Tuple<Packet, IPEndPoint>> _incomingPackets = new();

bool _networkRun = true;
Thread networkThread = new Thread(() => NetworkRun());
networkThread.Start();
Random random = new();

while (true) {

    bool hasPackets = _incomingPackets.TryDequeue(out var packetTuple);
    if (!hasPackets) {
        Thread.Sleep(0);
        continue;
    }
    var packet = packetTuple!.Item1;
    var cliEndpoint = packetTuple.Item2;

    Console.WriteLine(packet.PacketType);

    switch (packet.PacketType) {
        case PacketType.RequestJoin:
        _outgoingPackets.Enqueue(new(new RequestJoinAckPacket(), cliEndpoint));
        break;

        case PacketType.WaitingGame:
        if (!_playersToArenas.ContainsKey($"{cliEndpoint.Address}-{cliEndpoint.Port}")) {
            if (_playerInQueue != null) {
                var arena = new Arena(_playerInQueue, cliEndpoint);
                int seed = random.Next();
                _outgoingPackets.Enqueue(new(new GameStartPacket(seed), cliEndpoint));
                _outgoingPackets.Enqueue(new(new GameStartPacket(seed), _playerInQueue));
                _playersToArenas.TryAdd($"{cliEndpoint.Address}-{cliEndpoint.Port}", arena);
                _playersToArenas.TryAdd($"{_playerInQueue.Address}-{_playerInQueue.Port}", arena);
                _playerInQueue = null;
            }
            else {
                _playerInQueue = cliEndpoint;
            }
        }
        break;

        case PacketType.MyState:
        if (_playersToArenas.TryGetValue($"{cliEndpoint.Address}-{cliEndpoint.Port}", out var playersArena)) {
            _outgoingPackets.Enqueue(new((MyStatePacket)packet, cliEndpoint.Equals(playersArena.Player1) ? playersArena.Player2 : playersArena.Player1));
        }
        break;
    }
}

void NetworkRun() {
    while (_networkRun) {
        bool hasPackets = _outgoingPackets.TryDequeue(out var packetTuple);
        if (hasPackets) {
            var packetBytes = packetTuple!.Item1.GetBytes();
            listener.Send(packetBytes, packetBytes.Length, packetTuple.Item2);
        }
        if (listener.Available > 0) {
            IPEndPoint cliEndpoint = new(IPAddress.Any, 11000);
            Packet packet = PacketFactory.FromBytes(listener.Receive(ref cliEndpoint));
            _incomingPackets.Enqueue(new(packet, cliEndpoint));
        }
    }
}
