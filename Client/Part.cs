namespace Client;

public class Part(int i, int j, int diri, int dirj, int offset = 0) {
    public int I { get; set; } = i;
    public int J { get; set; } = j;
    public int DirI { get; set; } = diri;
    public int DirJ { get; set; } = dirj;
    public int Offset { get; set; } = offset;

    public void Move() {
        I += DirI;
        J += DirJ;

        if (I == 10) {
            I = 0;
        } else if (I < 0) {
            I = 9;
        }
        if (J == 10) {
            J = 0;
        } else if (J < 0) {
            J = 9;
        }
    }

    private (int, int, int, int, int, int, int, int) GetCursorPositions() {
        int topLeft = 3 + J * 6 + Offset;
        int topTop = 1 + 3 * I;

        int leftLeft = 2 + J * 6 + Offset;
        int leftTop = 2 + 3 * I;

        int rightLeft = 4 + J * 6 + Offset;
        int rightTop = 2 + I * 3;

        int bottomLeft = 3 + J * 6 + Offset;
        int bottomTop = 2 + I * 3;

        return (topLeft, topTop, leftLeft, leftTop, rightLeft, rightTop, bottomLeft, bottomTop);
    }

    public void Undisplay() {
        var pos = GetCursorPositions();
        var (Left, Top) = Console.GetCursorPosition();
        Console.SetCursorPosition(pos.Item1, pos.Item2);
        Console.Write(" ");
        Console.SetCursorPosition(pos.Item3, pos.Item4);
        Console.Write(" ");
        Console.SetCursorPosition(pos.Item5, pos.Item6);
        Console.Write(" ");
        Console.SetCursorPosition(pos.Item7, pos.Item8);
        Console.Write(" ");
        Console.SetCursorPosition(Left, Top);
    }

    public void Display() {
        var pos = GetCursorPositions();
        var (Left, Top) = Console.GetCursorPosition();
        Console.SetCursorPosition(pos.Item1, pos.Item2);
        Console.Write("_");
        Console.SetCursorPosition(pos.Item3, pos.Item4);
        Console.Write("|");
        Console.SetCursorPosition(pos.Item5, pos.Item6);
        Console.Write("|");
        Console.SetCursorPosition(pos.Item7, pos.Item8);
        Console.Write("_");
        Console.SetCursorPosition(Left, Top);
    }
}