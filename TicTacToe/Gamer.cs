namespace TicTacToe;

public static class Gamer
{
    public static ((int, int), bool) ComputerGamer(string[,] board, string player, int difficulty)
    {
        List<(int, int)> randomPlays = new List<(int, int)>();
        List<(int, int)> plays = new List<(int, int)>();

        int minmax = (player == "Y") ? int.MaxValue : int.MinValue;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == " ")
                {
                    board[i, j] = player;

                    randomPlays.Add((i, j));
                    TypeGamer(board, player, plays, ref minmax, i, j);

                    board[i, j] = " ";
                }
            }
        }

        return Play(board, randomPlays, plays, player, difficulty,
            (player == "X" && minmax == 1) || (player == "Y" && minmax == -1));
    }

    private static ((int, int), bool) Play(string[,] board, List<(int, int)> randomPLay, List<(int, int)> plays,
        string player,
        int difficulty, bool computerWin)
    {
        Random rnd = new Random();

        if (difficulty == 1) return (plays[rnd.Next(plays.Count)], computerWin);
        if (difficulty == 2) return (IntelligentPlay(board, plays, player), computerWin);

        return (randomPLay[rnd.Next(randomPLay.Count)], false);
    }

    private static (int, int) IntelligentPlay(string[,] board, List<(int, int)> plays, string player)
    {
        Random rnd = new Random();

        int mayor = 0;
        (int, int) play = plays[rnd.Next(plays.Count)];

        foreach (var item in plays)
        {
            board[item.Item1, item.Item2] = player;

            if (OnePlayForWin(board, player))
            {
                board[item.Item1, item.Item2] = " ";
                continue;
            }

            int cant = CantOptionsWin(board, player);

            if (mayor < cant)
            {
                play = item;
                mayor = cant;
            }

            if (mayor == cant)
            {
                if (rnd.Next(2) == 1) play = item;
            }

            board[item.Item1, item.Item2] = " ";
        }

        return play;
    }

    private static int CantOptionsWin(string[,] board, string player)
    {
        int cant = 0;

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == " ")
                {
                    if (player == "X")
                    {
                        board[i, j] = "Y";
                        if (GamerX(board) == 1) cant++;
                    }
                    else
                    {
                        board[i, j] = "X";
                        if (GamerY(board) == -1) cant++;
                    }

                    board[i, j] = " ";
                }
            }
        }

        return cant;
    }

    private static bool OnePlayForWin(string[,] board, string player)
    {
        bool win = false;

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == " ")
                {
                    board[i, j] = player;

                    if (Rules.GameState(board) == Rules.State.Win) win = true;

                    board[i, j] = " ";
                }

                if (win) return true;
            }
        }

        return false;
    }

    private static void TypeGamer(string[,] board, string player, List<(int, int)> plays, ref int minmax, int i, int j)
    {
        int aux;
        if (player == "Y")
        {
            aux = GamerX(board);
            if (minmax > aux)
            {
                minmax = aux;
                plays.Clear();
                plays.Add((i, j));
            }
        }
        else
        {
            aux = GamerY(board);
            if (minmax < aux)
            {
                minmax = aux;
                plays.Clear();
                plays.Add((i, j));
            }
        }

        if (minmax == aux) plays.Add((i, j));
    }

    private static int GamerX(string[,] board)
    {
        Rules.State game = Rules.GameState(board);
        if (game == Rules.State.Win) return -1;
        if (game == Rules.State.EndGame) return 0;
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
        if (game == Rules.State.Win) return 1;
        if (game == Rules.State.EndGame) return 0;
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