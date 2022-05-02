using TicTacToe;

string[,] board = new string[3, 3];

for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 3; j++)
    {
        board[i, j] = " ";
    }
}

string player = Players();
if (player == "X") Gamer.ComputerGamer(board, player);
PrintBoard(board);

while (true)
{
    PlayerUser(player, board);
    Gamer.ComputerGamer(board, player);
    PrintBoard(board);
    Rules.State state = Rules.GameState(board);
    if (state == Rules.State.win)
    {
        Console.WriteLine("Perdiste");
        break;
    }
    if (state == Rules.State.endGame)
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
static string Players()
{
    Console.WriteLine("Presione \'x\' para ser el 1er Jugador o \'y\' para ser el 2do Jugador");
    string s = Console.ReadLine()!;
    if (s == "x") return "Y";
    return "X";
}

static bool Wrong(string[] s, string[,] board, ref int row, ref int column)
{
    if (!int.TryParse(s[0], out row) || !int.TryParse(s[1], out column)) return false;
    if (row < 0 || column < 0) return false;
    if (row >= board.GetLength(0) || column > board.GetLength(1)) return false;
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
