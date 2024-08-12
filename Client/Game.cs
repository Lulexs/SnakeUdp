
namespace Client;

class Game {
    private readonly Board _board = new();

    private readonly Snake _mySnake = new();
    private Part _myFood;
    private int _myScore = 0;
    private readonly Snake _oppSnake = new();
    private int _oppScore = 0;
    private Part _oppFood;
    private readonly Random _random;

    public Game(int seed) {
        Console.WriteLine(seed);
        _random = new(seed);
        var (foodI, foodJ) = GenerateFood();
        _myFood = new(foodI, foodJ, 0, 0);
        _oppFood = new(foodI, foodJ, 0, 0);
    }

    public Tuple<int, int> GenerateFood() {
        int foodI = _random.Next(0, 10);
        int foodJ = _random.Next(0, 10);

        while (_mySnake.Has(foodI, foodJ)) {
            foodI = _random.Next(0, 10);
            foodJ = _random.Next(0, 10);
        }

        return new Tuple<int, int>(foodI, foodJ);
    }

    public void InitDraw() {
        _board.Draw();
        _mySnake.Display();
        _myFood.Display();
    }

    public void Move() {
        _mySnake.Move();

        if (_mySnake.head.I == _myFood.I && _mySnake.head.J == _myFood.J) {
            _mySnake.Extend();
            _myScore += 1;
            
            var (foodI, foodJ) = GenerateFood();
            _myFood = new(foodI, foodJ, 0, 0);
            _myFood.Display();
        }
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