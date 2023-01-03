using TicTacToe;

string[,] board = new string[3, 3];

for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 3; j++)
    {
        board[i, j] = " ";
    }
}

(string, int) aux = Players();
int difficulty = aux.Item2;
string player = aux.Item1;

if (player == "X") Gamer.ComputerGamer(board, player, difficulty);
PrintBoard(board);

while (true)
{
    PlayerUser(player, board);
    PrintBoard(board);

    Rules.State state = Rules.GameState(board);
    if (state == Rules.State.Win)
    {
        Console.WriteLine("Ganaste");
        break;
    }

    if (state == Rules.State.EndGame)
    {
        Console.WriteLine("Fin del juego");
        break;
    }

    (var play,bool computerWin) = Gamer.ComputerGamer(board, player, difficulty);
    board[play.Item1, play.Item2] = player == "X" ? "X" : "Y";
    PrintBoard(board);

    state = Rules.GameState(board);
    if (state == Rules.State.Win)
    {
        Console.WriteLine("Perdiste");
        break;
    }

    if (state == Rules.State.EndGame)
    {
        Console.WriteLine("Fin del juego");
        break;
    }
}

static void PlayerUser(string player, string[,] board)
{
    int row = -1;
    int column = -1;
    while (true)
    {
        Console.Write("Entre las coordenadas de su jugada: ");
        string[] s = Console.ReadLine()!.Split();
        if (!Wrong(s, board, ref row, ref column))
        {
            Console.WriteLine("Jugada Incorrecta");
        }
        else
        {
            break;
        }
    }

    board[row, column] = (player == "X") ? "Y" : "X";
}

static (string, int) Players()
{
    string s = "";

    while (s != "x" && s != "y")
    {
        Console.Clear();
        Console.WriteLine("Presione \'x\' para ser el 1er Jugador o \'y\' para ser el 2do Jugador");
        s = Console.ReadLine()!;
    }

    int difficulty = -1;

    while (difficulty == -1)
    {
        Console.Clear();
        Console.WriteLine("Seleccione el nivel de dificultad del 0 al 2");
        string n = Console.ReadLine()!;

        int.TryParse(n, out difficulty);
        if (difficulty < 0 || difficulty > 2) difficulty = -1;
    }

    if (s == "x") return ("Y", difficulty);
    return ("X", difficulty);
}

static bool Wrong(string[] s, string[,] board, ref int row, ref int column)
{
    if (s.Length != 2) return false;
    if (!int.TryParse(s[0], out row) || !int.TryParse(s[1], out column)) return false;
    if (row < 0 || column < 0) return false;
    if (row >= board.GetLength(0) || column >= board.GetLength(1)) return false;
    if (board[row, column] != " ") return false;
    return true;
}

static void PrintBoard(string[,] board)
{
    Console.Clear();
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            if (board[i, j] != " ") Console.Write(board[i, j] + " ");
            else Console.Write("_ ");
        }

        Console.WriteLine();
    }

    Console.WriteLine();
}