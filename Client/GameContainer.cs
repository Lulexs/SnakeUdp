using System.Collections.Concurrent;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using Common.Packets;

namespace Client;

class GameContainer {
    private readonly string _startingScreen = @"
                                    /^\/^\
                                    _|__|  O|
                            \/     /~     \_/ \
                            \____|__________/  \
                                    \_______      \
                                            `\     \                 \
                                            |     |                  \
                                            /      /                    \
                                            /     /                       \\
                                        /      /                         \ \
                                        /     /                            \  \
                                    /     /             _----_            \   \
                                    /     /           _-~      ~-_         |   |
                                    (      (        _-~    _--_    ~-_     _/   |
                                    \      ~-____-~    _-~    ~-_    ~-_-~    /
                                        ~-_           _-~          ~-_       _-~
                                        ~--______-~                ~-___-~
                                __          __  _                            _                                      
                                \ \        / / | |                          | |                                     
                                 \ \  /\  / /__| | ___ ___  _ __ ___   ___  | |_ ___                                
                                  \ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \                               
                                   \  /\  /  __/ | (_| (_) | | | | | |  __/ | || (_) |                              
  __  __ _    _ _   _______ _____ __\/_ \/ \___|_|\___\___/|_|_|_|_|_|\___|  \__\___/_   _ ______ _  __ _____ _____ 
 |  \/  | |  | | | |__   __|_   _|  __ \| |        /\\ \   / /  ____|  __ \   / ____| \ | |  ____| |/ // ____|_   _|
 | \  / | |  | | |    | |    | | | |__) | |       /  \\ \_/ /| |__  | |__) | | (___ |  \| | |__  | ' /| (___   | |  
 | |\/| | |  | | |    | |    | | |  ___/| |      / /\ \\   / |  __| |  _  /   \___ \| . ` |  __| |  <  \___ \  | |  
 | |  | | |__| | |____| |   _| |_| |    | |____ / ____ \| |  | |____| | \ \   ____) | |\  | |____| . \ ____) |_| |_ 
 |_|  |_|\____/|______|_|  |_____|_|    |______/_/    \_\_|  |______|_|  \_\ |_____/|_| \_|______|_|\_\_____/|_____|

                                                Press any key to play                                                                                          
                                                                                                                    
    ";

    private readonly string _waitingScreen = @"
   _____ ______          _____   _____ _    _ _____ _   _  _____   ______ ____  _____     ____  _____  _____   ____  _   _ ______ _   _ _______              
  / ____|  ____|   /\   |  __ \ / ____| |  | |_   _| \ | |/ ____| |  ____/ __ \|  __ \   / __ \|  __ \|  __ \ / __ \| \ | |  ____| \ | |__   __|             
 | (___ | |__     /  \  | |__) | |    | |__| | | | |  \| | |  __  | |__ | |  | | |__) | | |  | | |__) | |__) | |  | |  \| | |__  |  \| |  | |                
  \___ \|  __|   / /\ \ |  _  /| |    |  __  | | | | . ` | | |_ | |  __|| |  | |  _  /  | |  | |  ___/|  ___/| |  | | . ` |  __| | . ` |  | |                
  ____) | |____ / ____ \| | \ \| |____| |  | |_| |_| |\  | |__| | | |   | |__| | | \ \  | |__| | |    | |    | |__| | |\  | |____| |\  |  | |     _   _   _  
 |_____/|______/_/    \_\_|  \_\\_____|_|  |_|_____|_| \_|\_____|_|_| ___\____/|_|  \_\  \____/|_|____|_|_____\____/|_| \_|______|_| \_|  |_|    (_) (_) (_) 
                                |  __ \| |    |  ____|   /\    / ____|  ____| \ \        / /\   |_   _|__   __|                                              
                                | |__) | |    | |__     /  \  | (___ | |__     \ \  /\  / /  \    | |    | |                                                 
                                |  ___/| |    |  __|   / /\ \  \___ \|  __|     \ \/  \/ / /\ \   | |    | |                                                 
                                | |    | |____| |____ / ____ \ ____) | |____     \  /\  / ____ \ _| |_   | |                                                 
                                |_|    |______|______/_/    \_\_____/|______|     \/  \/_/    \_\_____|  |_|                                                 


";

    private bool _redraw = true;
    private GameState _gameState = GameState.Lobby;
    // private Game _game = new(5);
    private Game? _game = null;
    private readonly UdpClient _client = new();
    private IPEndPoint _serverEndpoint = new(IPAddress.Loopback, 11000);
    private readonly ConcurrentQueue<Packet> _outgoingPackets = new();
    private readonly ConcurrentQueue<Packet> _incomingPackets = new();
    private int _oppScore = 0;
    private int _myScore = 0;

    private Thread _networkThread = null!;
    private bool _networkRun = true;

    private void NetworkRun() {
        while (_networkRun) {
            bool hasPackets = _outgoingPackets.TryDequeue(out var packet);
            if (hasPackets) {
                byte[] bytes = packet!.GetBytes();
                _client.Send(bytes, bytes.Length, _serverEndpoint);
            }
            if (_client.Available > 0) {
                byte[] bytes = _client.Receive(ref _serverEndpoint);
                _incomingPackets.Enqueue(PacketFactory.FromBytes(bytes));
            }
        }
    }

    private void RestartGame() {
        Console.WriteLine("HereRestart");
    }

    public void Run() {
        bool run = true;

        while (run) {
            if (Console.KeyAvailable) {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                Progress(consoleKeyInfo.KeyChar);
            }

            bool recvdPacket = _incomingPackets.TryDequeue(out var packet);
            if (recvdPacket) {
                if (_gameState == GameState.Connecting && packet!.PacketType == PacketType.RequestJoinAck) {
                    _gameState = GameState.WaitingForOpponent;
                    _outgoingPackets.Enqueue(new WaitingGamePacket());
                }
                else if (_gameState == GameState.Connecting) {
                    RestartGame();
                    return;
                }

                else if (_gameState == GameState.WaitingForOpponent && packet!.PacketType == PacketType.GameStart) {
                    _gameState = GameState.InGame;
                    _game = new(((GameStartPacket)packet).Seed);
                    _redraw = true;
                }
                else if (_gameState == GameState.WaitingForOpponent) {
                    RestartGame();
                    return;
                }

                else if ((_gameState == GameState.InGame || _gameState == GameState.WaitingFinish) && packet!.PacketType == PacketType.MyState) {
                    _game!.OpponetMove((MyStatePacket)packet);
                }
                else if (_gameState == GameState.InGame && packet!.PacketType == PacketType.Bye) {
                    _oppScore = ((ByePacket)packet).Score;
                }
                else if (_gameState == GameState.WaitingFinish && packet!.PacketType == PacketType.Bye) {
                    _oppScore = ((ByePacket)packet).Score;
                    _gameState = GameState.Finished;
                    _redraw = true;
                }
            }
            if (_game is not null && _game.Move()) {
                _outgoingPackets.Enqueue(new MyStatePacket(_game.GameState()));
                _myScore = _game.Score;
            }
            else if (_game is not null) {
                _outgoingPackets.Enqueue(new ByePacket(_game.Score));
                _gameState = GameState.WaitingFinish;
                _game = null;
            }

            Display();
            Thread.Sleep(500);
        }
    }

    private void Progress(char c) {
        switch (_gameState) {
            case GameState.Lobby:
            _gameState = GameState.Connecting;
            _outgoingPackets.Enqueue(new RequestJoinPacket());
            _networkThread = new Thread(() => NetworkRun());
            _networkThread.Start();
            break;

            case GameState.InGame:
            _game?.ChangeDir(c);
            break;
        }
        _redraw = true;
    }

    private void Display() {
        if (!_redraw)
            return;
        _redraw = false;
        Console.Clear();
        switch (_gameState) {
            case GameState.Lobby:
            Console.WriteLine(_startingScreen);
            break;

            case GameState.Connecting:
            case GameState.WaitingForOpponent:
            Console.WriteLine(_waitingScreen);
            break;

            case GameState.InGame:
            _game?.InitDraw();
            break;

            case GameState.Finished:
            if (_myScore > _oppScore) {
                Console.WriteLine("You won!");
            }
            else if (_myScore == _oppScore) {
                Console.WriteLine("Draw!");
            }
            else {
                Console.WriteLine("You lost!");
            }
            Console.WriteLine("Press any key to continue");
            break;
        }
    }
}