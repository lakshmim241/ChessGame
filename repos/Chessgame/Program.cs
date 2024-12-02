// See https://aka.ms/new-console-template for more information

using System;

namespace Games
{
    class Program
    {
        static void Main()
        {
            var game = new ChessGame();
            game.Start();
        }
    }

    public class ChessGame
    {
        private readonly ChessBoard _board;
        private readonly ChessPlayer _player;
        private int _moves;

        public ChessGame()
        {
            _board = new ChessBoard(8, 8);
            _player = new ChessPlayer("A1");
            _moves = 0;
        }

        public void Start()
        {
            Console.WriteLine("Use 'up', 'down', 'left', 'right'.\n");

            while (!_player.HasReachedGoal() && _player.Lives > 0)
            {
                Console.WriteLine($"Position: {_player.Position}, Lives: {_player.Lives}, Moves: {_moves}");
                Console.Write("Enter move (up/down/left/right): ");
                var input = Console.ReadLine()?.ToLower();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid input.");
                    return;
                }
                if (IsValidMove(input))
                {
                    _player.Move(input);
                    _moves++;
                    if (_board.HasMine(_player.Position))
                    {
                        Console.WriteLine("You hit a mine");
                        _player.LoseLife();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move. Try again.");
                }
            }

            if (_player.Lives > 0)
            {
                Console.WriteLine($"You reached the goal in {_moves} moves.");
            }
            else
            {
                Console.WriteLine(" You ran out of lives.");
            }
        }

        private static bool IsValidMove(string input)
        {
            return input == "up" || input == "down" || input == "left" || input == "right";
        }
    }

    public class ChessBoard
    {
        private readonly int _rows;
        private readonly int _cols;
        private readonly bool[,] _mines;

        public ChessBoard(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _mines = new bool[rows, cols];
            GenerateMines();
        }

        public bool HasMine(string position)
        {
            var (row, col) = ConvertPositionToIndex(position);
            return _mines[row, col];
        }

        private void GenerateMines()
        {
            var random = new Random();
            for (int i = 0; i < 10; i++) 
            {
                int row = random.Next(_rows);
                int col = random.Next(_cols);
                _mines[row, col] = true;
            }
        }

        private (int row, int col) ConvertPositionToIndex(string position)
        {
            int col = position[0] - 'A';
            int row = int.Parse(position[1].ToString()) - 1;
            return (row, col);
        }
    }

    public class ChessPlayer
    {
        private int _row;
        private int _col;
        public int Lives { get;  set; }

        public string Position => $"{(char)(_col + 'A')}{_row + 1}";

        public ChessPlayer(string startPosition)
        {
            (_row, _col) = ConvertPositionToIndex(startPosition);
            Lives = 3;
        }

        public void Move(string direction)
        {
            switch (direction)
            {
                case "up": _row = Math.Max(0, _row - 1); break;
                case "down": _row = Math.Min(7, _row + 1); break;
                case "left": _col = Math.Max(0, _col - 1); break;
                case "right": _col = Math.Min(7, _col + 1); break;
            }
        }

        public void LoseLife()
        {
            Lives--;
        }

        public bool HasReachedGoal()
        {
            return _row == 7 && _col == 7;
        }

        private (int row, int col) ConvertPositionToIndex(string position)
        {
            int col = position[0] - 'A';
            int row = int.Parse(position[1].ToString()) - 1;
            return (row, col);
        }
    }
}
