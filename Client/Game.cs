
namespace Client;

class Game {
    private readonly Board _board = new();

    private readonly Snake _mySnake = new();
    private Part _myFood = new(0, 0, 0, 0); // TODO INIT FOOD
    private readonly Snake _oppSnake = new();
    private Part _oppFood = new(0, 0, 0, 0); // TOOD OPP FOOD
    private readonly Random _random;

    public Game() {
        _random = new(1);
    }

    public void InitDraw() {
        _board.Draw();
        _mySnake.Display();
    }

    public void Move() {
        _mySnake.Move();
    }

    public void ChangeDir(char c) {
        if (c == 'w' || c == 'W') {
            _mySnake.ChangeDir(-1, 0);
        }
        else if (c == 's' || c == 'S') {
            _mySnake.ChangeDir(1, 0);
        }
        else if (c == 'a' || c == 'A') {
            _mySnake.ChangeDir(0, -1);
        }
        else if (c == 'd' || c == 'D') {
            _mySnake.ChangeDir(0, 1);
        }
    }
    
}