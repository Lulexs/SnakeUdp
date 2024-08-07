using System.Text;

namespace Client;

internal class Board {
    private int dim;
    private string board;
    
    public Board(int dim = 10) {
        this.dim = dim;
        board = DrawBoards(this.dim);

    }

    private string DrawTopLine(int dim) {
        StringBuilder sb = new();

        for (int i = 0; i < dim; ++i) {
            sb.Append(" _____");
        }
        sb.Append("           ");
        for (int i = 0; i < dim; ++i) {
            sb.Append(" _____");
        }
        sb.AppendLine();

        return sb.ToString();
    }

    private string DrawMiddleLines(int dim) {
        StringBuilder sb = new();

        for (int i = 0; i < 2; ++i) {
            sb.Append("|");
            for (int j = 0; j < dim; ++j) {
                sb.Append("     |");
            }
            sb.Append("          ");
            sb.Append("|");
            for (int j = 0; j < dim; ++j) {
                sb.Append("     |");
            }
            sb.AppendLine();
        }
        sb.Append("|");
        for (int i = 0; i < dim; ++i) {
            sb.Append("_____|");
        }
        sb.Append("          ");
        sb.Append("|");
        for (int i = 0; i < dim; ++i) {
            sb.Append("_____|");
        }
        sb.AppendLine();

        return sb.ToString();
    }

    private string DrawBoards(int dim) {
        StringBuilder sb = new();

        sb.Append(DrawTopLine(dim));

        for (int k = 0; k < dim; ++k) {
            sb.Append(DrawMiddleLines(dim));
        }

        return sb.ToString();
    }

    public void Draw() {
        Console.WriteLine(board);
    }
        
}