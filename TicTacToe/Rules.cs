namespace TicTacToe;

public static class Rules
{
    public enum State
    {
        Win,
        ContinueGame,
        EndGame,
    }

    public static State GameState(string[,] board)
    {
        State game = Rows(board);
        if (game != State.ContinueGame) return game;
        game = Columns(board);
        if (game != State.ContinueGame) return game;
        game = End(board);
        if (game != State.ContinueGame) return game;
        game = Diagonal(board);
        if (game != State.ContinueGame) return game;
        return State.ContinueGame;
    }

    private static State Rows(string[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (board[i, 0] == " ") continue;
            bool win = true;
            for (int j = 1; j < board.GetLength(1); j++)
            {
                if (board[i, 0] != board[i, j]) win = false;
            }

            if (win) return State.Win;
        }

        return State.ContinueGame;
    }

    private static State Columns(string[,] board)
    {
        for (int i = 0; i < board.GetLength(1); i++)
        {
            if (board[0, i] == " ") continue;
            bool win = true;
            for (int j = 1; j < board.GetLength(0); j++)
            {
                if (board[0, i] != board[j, i]) win = false;
            }

            if (win) return State.Win;
        }

        return State.ContinueGame;
    }

    private static State End(string[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == " ") return State.ContinueGame;
            }
        }

        return State.EndGame;
    }

    private static State Diagonal(string[,] board)
    {
        bool winLeft = (board[0, 0] != " ");
        bool winRight = (board[0, board.GetLength(1) - 1] != " ");
        for (int i = 1; i < board.GetLength(0); i++)
        {
            if (board[0, 0] != board[i, i]) winLeft = false;
            if (board[0, board.GetLength(1) - 1] != board[i, board.GetLength(1) - 1 - i]) winRight = false;
        }

        if (winLeft || winRight) return State.Win;
        return State.ContinueGame;
    }
}