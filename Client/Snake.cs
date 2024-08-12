
namespace Client;

public class Snake {
    private readonly List<Part> _parts = [];
    private readonly Part head;

    public Snake() {
        head = new(4, 4, 1, 0);
        _parts.Add(head);
    }

    public void Display() {
        var oldPos = Console.GetCursorPosition();
        Console.SetCursorPosition(3, 1);
        Console.Write("_");
        Console.SetCursorPosition(2, 2);
        Console.Write("|");
        Console.SetCursorPosition(4, 2);
        Console.Write("|");
        Console.SetCursorPosition(3, 2);
        Console.Write("_");
        Console.SetCursorPosition(oldPos.Left, oldPos.Top);
    }

}