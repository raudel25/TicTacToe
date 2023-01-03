using System.Drawing;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Brushes = Avalonia.Media.Brushes;
using TicTacToe;

namespace GameUI;

public partial class MainWindow : Window
{
    private string _player;
    private bool _end;
    private int _difficulty;
    private Button[,] _boardControl;
    private string[,] _board;

    public MainWindow()
    {
        InitializeComponent();
        this._player = "X";
        this._boardControl = new Button[3, 3];
        this._board = new string[3, 3];
        this._end = false;
    }

    private void PlayerX(object? sender, RoutedEventArgs e)
    {
        this._player = "X";
        Menu.IsVisible = false;
        Game.IsVisible = true;
        DeterminateDifficulty();
        CreateBoard();
    }

    private void PlayerY(object? sender, RoutedEventArgs e)
    {
        this._player = "Y";
        Menu.IsVisible = false;
        Game.IsVisible = true;
        DeterminateDifficulty();
        CreateBoard();
        PlayComputer();
    }

    private void DeterminateDifficulty()
    {
        if (Izi.IsChecked.Equals(true)) this._difficulty = 0;
        if (Medium.IsChecked.Equals(true)) this._difficulty = 1;
        if (Hard.IsChecked.Equals(true)) this._difficulty = 2;
    }

    private void CreateBoard()
    {
        this._end = false;
        GameState.Text = "";
        Board.Children.Clear();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var aux = new Button();
                aux.Width = 100;
                aux.Height = 100;
                aux.Content = " ";
                aux.FontSize = 30;
                aux.HorizontalContentAlignment = HorizontalAlignment.Center;
                aux.VerticalContentAlignment = VerticalAlignment.Center;
                aux.Background = Brushes.Silver;
                aux.Click += PlayPlayer;

                Grid.SetRow(aux, i);
                Grid.SetColumn(aux, j);
                Board.Children.Add(aux);

                this._boardControl[i, j] = aux;
                this._board[i, j] = " ";
            }
        }
    }

    private void PlayPlayer(object? sender, RoutedEventArgs e)
    {
        if (!_end) GameState.Text = "";
        (int row, int column) = (0, 0);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (this._boardControl[i, j].Equals(sender))
                {
                    (row, column) = (i, j);
                    break;
                }
            }
        }


        if (this._board[row, column] == " " && !this._end)
        {
            this._boardControl[row, column].Content = this._player;
            this._board[row, column] = this._player;
            PlayComputer();
        }
        else if (this._board[row, column] != " " && !this._end) GameState.Text = "Jugada Inválida";
    }

    private void PlayComputer()
    {
        Rules.State state = Rules.GameState(this._board);

        if (state == Rules.State.Win)
        {
            GameState.Text = "Has Ganado";
            this._end = true;
            return;
        }
        
        if (state == Rules.State.EndGame)
        {
            this._end = true;
            GameState.Text = "Fin del juego";
            return;
        }

        (var play,bool computerWin) = Gamer.ComputerGamer(this._board, this._player == "X" ? "Y" : "X", this._difficulty);
        this._board[play.Item1, play.Item2] = this._player == "X" ? "Y" : "X";
        this._boardControl[play.Item1, play.Item2].Content = this._player == "X" ? "Y" : "X";

        state = Rules.GameState(this._board);

        if (state == Rules.State.Win)
        {
            this._end = true;
            GameState.Text = "Perdiste";
            return;
        }

        if (state == Rules.State.EndGame)
        {
            this._end = true;
            GameState.Text = "Fin del juego";
            return;
        }

        if (computerWin) GameState.Text = "Vas a perder...";
    }

    private void NewGame(object? sender, RoutedEventArgs e)
    {
        Game.IsVisible = false;
        Menu.IsVisible = true;
    }
}