namespace ConnectFour
{
    class Model
    {
        public char[,] Board { get; set; }
        public int CurrentPlayer { get; set; } // Either 0 or 1
        private List<bool> IsColumnFull = new List<bool>(); // True if column is full
        public int? Winner { get; private set; } // Null if no winner 
        public Model()
        {
            int row = 6;
            int column = 7;
            Board = new char[row, column]; // # for empty, X for Player 1, O for Player 2
            // Fill up the board with #
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (i == 0) IsColumnFull.Add(false);
                    Board[i, j] = '#';
                }
            }
            CurrentPlayer = 0;
        }

        public void MakeMove(Player player, int column)
        {
            column = column - 1;
            // If column is full, do nothing and return
            if (IsColumnFull[column])
                return;

            // Place a disc in a column
            for (int i = Board.GetLength(0) - 1; i >= 0; i--)
            {
                if (Board[i, column] == '#')
                {
                    Board[i, column] = player.PlayerSymbol;
                    if (i == 0) IsColumnFull[column] = true;
                    break;
                }
            }
            CurrentPlayer = (CurrentPlayer == 0) ? 1 : 0; // Swap current player
            return;
        }

        public bool IsGameOver()
        {
            // 1. Find Winner, 2. Check tie
            char CheckSymbol = (CurrentPlayer == 0) ? 'O' : 'X';    // Only check the symbol of the previous move
            if (HorizontalWin(CheckSymbol) || VertWin(CheckSymbol) || DiagWin(CheckSymbol) || CrossDiagWin(CheckSymbol))
            {
                Winner = (CheckSymbol == 'X') ? 0 : 1;
                return true;
            }
            return IsTie();
        }

        private bool VertWin(char symbol)
        {
            for (int j = 0; j < Board.GetLength(1); j++)
                for (int i = Board.GetLength(0) - 1; i >= Board.GetLength(0) - 3; i--)
                    if (Board[i, j] == symbol && Board[i - 1, j] == symbol && Board[i - 2, j] == symbol && Board[i - 3, j] == symbol)
                    {
                        Console.WriteLine("Vertical");
                        return true;
                    }
            return false;
        }
        private bool CrossDiagWin(char symbol)
        {
            for (int i = 0; i <= Board.GetLength(0) - 4; i++) // 0, 1, 2
                for (int j = Board.GetLength(1) - 4; j < Board.GetLength(1); j++) // 3, 4, 5, 6
                    if (Board[i, j] == symbol && Board[i + 1, j - 1] == symbol && Board[i + 2, j - 2] == symbol && Board[i + 3, j - 3] == symbol)
                        return true;
            return false;
        }

        private bool DiagWin(char symbol)
        {
            for (int i = 0; i <= Board.GetLength(0) - 4; i++) // 0, 1, 2
                for (int j = 0; j <= Board.GetLength(1) - 4; j++) // 0, 1, 2, 3
                    if (Board[i, j] == symbol && Board[i + 1, j + 1] == symbol && Board[i + 2, j + 2] == symbol && Board[i + 3, j + 3] == symbol)
                        return true;

            return false;
        }

        private bool HorizontalWin(char symbol)
        {
            for (int i = 0; i < Board.GetLength(0); i++)
                for (int j = 0; j < Board.GetLength(1) - 3; j++)
                    if (Board[i, j] == symbol && Board[i, j + 1] == symbol && Board[i, j + 2] == symbol && Board[i, j + 3] == symbol)
                    {
                        Console.WriteLine("Horizontal");
                        return true;
                    }
            return false;
        }

        private bool IsTie()
        {   // Return true if all columns are full
            return IsColumnFull.All(x => x);
        }

    }
}
