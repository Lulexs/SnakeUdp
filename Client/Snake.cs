
namespace Client;

public class Snake {
    private readonly List<Part> _parts = [];
    public Part head { get; set; }
    private int diri = 1;
    private int dirj = 0;
    private int _offset = 0; 
    
    private readonly Dictionary<string, Tuple<int, int>> turns = [];

    public Queue<Tuple<int, int>> Undisplays = [];
    public Queue<Tuple<int, int>> Displays = []; 
    

    public Snake(int offset = 0) {
        _offset = offset;
        head = new(4, 4, diri, dirj, _offset);
        _parts.Add(head);
    }

    public bool Has(int i, int j) {
        foreach (var part in _parts) {
            if (part.I == i && part.J == j)
                return true;
        }
        return false;
    }

    public void Extend() {
        var last = _parts.Last();
        _parts.Add(new Part(last.I - last.DirI, last.J - last.DirJ, last.DirI, last.DirJ, _offset));
        Undisplays.Enqueue(new(last.I - last.DirI, last.J - last.DirJ));
        Displays.Enqueue(new(last.I - last.DirI, last.J - last.DirJ));
    }

    public void Move() {
        Undisplays.Enqueue(new(_parts.Last().I, _parts.Last().J));
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
        Displays.Enqueue(new(head.I, head.J));
        head.Display();
    }

    public void Display() {
        foreach(var part in _parts) {
            part.Display();
        }
    }

    public void ChangeDir(int diri, int dirj) {
        if (diri == -head.DirI || dirj == -head.DirJ)
            return;
        turns.Add($"{head.I}-{head.J}", new Tuple<int, int>(diri, dirj));
        head.DirI = diri;
        head.DirJ = dirj;
    }

}