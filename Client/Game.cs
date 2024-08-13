
namespace Client;

class Game {
    private readonly Board _board = new();
    private readonly Snake _mySnake = new();
    private Part _myFood;
    private readonly Random _random;

    public Game(int seed) {
        Console.WriteLine(seed);
        _random = new(seed);
        var (foodI, foodJ) = GenerateFood();
        _myFood = new(foodI, foodJ, 0, 0);
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

    public bool Move() {
        _mySnake.Move();

        if (_mySnake.head.I == _myFood.I && _mySnake.head.J == _myFood.J) {
            _mySnake.Extend();
            
            var (foodI, foodJ) = GenerateFood();
            _myFood = new(foodI, foodJ, 0, 0);
            _myFood.Display();
            return true;
        }
        return true;
    }

    public Tuple<int, int, int, int, int, int> GameState() {
        var undisplay = _mySnake.Undisplays.Dequeue();
        var displays = _mySnake.Displays.Dequeue();
        return new Tuple<int, int, int, int, int, int>(_myFood.I, _myFood.J, undisplay.Item1, undisplay.Item2, displays.Item1, displays.Item2);
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