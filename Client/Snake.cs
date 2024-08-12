
namespace Client;

public class Snake {
    private readonly List<Part> _parts = [];
    private readonly Part head;
    private int diri = 1;
    private int dirj = 0;
    
    private readonly Dictionary<string, Tuple<int, int>> turns = [];
    

    public Snake() {
        head = new(4, 4, diri, dirj);
        _parts.Add(head);
    }

    public void Move() {
        _parts.Last().Undisplay();
        foreach(var part in _parts) {
            var posString = $"{part.I}-{part.J}";
            if (turns.TryGetValue(posString, out var val)) {
                part.DirI = val.Item1;
                part.DirJ = val.Item2;
                if (part == _parts.Last()) {
                    turns.Remove(posString);
                }
            }
            part.Move();
            
        }
        head.Display();
    }

    public void Display() {
        foreach(var part in _parts) {
            part.Display();
        }
    }

    public void ChangeDir(int diri, int dirj) {
        turns.Add($"{head.I}-{head.J}", new Tuple<int, int>(diri, dirj));
        head.DirI = diri;
        head.DirJ = dirj;
    }

}