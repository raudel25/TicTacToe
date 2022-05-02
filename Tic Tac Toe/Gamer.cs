namespace TicTacToe;

public static class Gamer
{
    public static void ComputerGamer(string[,] board, string player)
    {
        (int, int) game = (0, 0);
        int minmax = (player == "Y") ? int.MaxValue : int.MinValue;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == " ")
                {
                    board[i, j] = player;
                    int aux = 0;
                    if (player == "Y")
                    {
                        aux = GamerX(board);
                        if (minmax > aux)
                        {
                            minmax = aux;
                            game = (i, j);
                        }
                    }
                    else
                    {
                        aux = GamerY(board);
                        if (minmax < aux)
                        {
                            minmax = aux;
                            game = (i, j);
                        }
                    }
                    if (minmax == aux)
                    {
                        Random rnd = new Random();
                        if (rnd.Next() % 2 == 0) game = (i, j);
                    }
                    board[i, j] = " ";
                }
            }
        }
        board[game.Item1, game.Item2] = player;
    }
    private static int GamerX(string[,] board)
    {
        Rules.State game = Rules.GameState(board);
        if (game == Rules.State.win) return -1;
        if (game == Rules.State.endGame) return 0;
        int minmax = int.MinValue;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == " ")
                {
                    board[i, j] = "X";
                    minmax = Math.Max(minmax, GamerY(board));
                    board[i, j] = " ";
                    if (minmax == 1) return 1;
                }
            }
        }
        return minmax;
    }
    private static int GamerY(string[,] board)
    {
        Rules.State game = Rules.GameState(board);
        if (game == Rules.State.win) return 1;
        if (game == Rules.State.endGame) return 0;
        int minmax = int.MaxValue;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == " ")
                {
                    board[i, j] = "Y";
                    minmax = Math.Min(minmax, GamerX(board));
                    board[i, j] = " ";
                    if (minmax == -1) return -1;
                }
            }
        }
        return minmax;
    }
}
