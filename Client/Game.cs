
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

    
}