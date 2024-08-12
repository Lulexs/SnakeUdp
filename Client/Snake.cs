
namespace Client;

public class Snake {
    private readonly List<Part> _parts = [];
    private readonly Part head;
    private int diri = 1;
    private int dirj = 0;
    

    public Snake() {
        head = new(4, 4, diri, dirj);
        _parts.Add(head);
    }

    public void Move() {
        _parts.Last().Undisplay();
        foreach(var part in _parts) {
            part.Move();
        }
        head.Display();
    }

    public void Display() {
        foreach(var part in _parts) {
            part.Display();
        }
    }

}