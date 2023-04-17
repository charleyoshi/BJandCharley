namespace ConnectFour
{
    class UserIO
    {// interacts and communicates via text input and Console output

        // StartScreen, PrintBoard, PrintWinner, PrintTie, getName, PrintTurn, PromptHumanColumn
        public static int StartScreen()
        {
            Console.Clear();
            int playerCount = 0;
            Console.WriteLine("Welcome to Connect Four! 1-Player or 2-Player?");
            while (playerCount != 1 && playerCount != 2)
                try
                {
                    playerCount = Convert.ToInt32(Console.ReadLine());
                    if (playerCount != 1 && playerCount != 2) Console.Write("Please enter 1 or 2: ");
                }
                catch (Exception) { Console.Write("Please enter 1 or 2: "); }
            return playerCount;
        }

        public static void PrintBoard(Model game)
        {
            Console.Clear();
            PrintBoardUtil(game);
        }
        public static void PrintBoard(Model game, List<Player> players)
        {
            // On computer's move, stay on screen to enhance user experience
            if (players[game.CurrentPlayer] is Human)
                Console.Clear(); 
            PrintBoardUtil(game);
        }
        public static void PrintBoardUtil(Model game)
        {
            Console.WriteLine();
            for (int i = 0; i < game.Board.GetLength(0); i++)
            {
                Console.Write("|  ");
                for (int k = 0; k < game.Board.GetLength(1); k++)
                {
                    if (game.Board[i, k] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(game.Board[i, k]);
                        Console.ResetColor();
                    }
                    else if (game.Board[i, k] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(game.Board[i, k]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(game.Board[i, k]);
                    }

                    Console.Write("  ");
                }
                Console.WriteLine("| ");
            }
            Console.WriteLine("   1  2  3  4  5  6  7  ");
        }

        public static void PrintWinner(Player player)
        {
            Console.WriteLine($"It is a Connect 4. {player.PlayerName} Wins!");
        }

        public static void PrintTie()
        {
            Console.WriteLine("Game is a Tie!");
        }

        public static string getName(int? order = null)
        {
            Console.Write($"Enter {(order == null ? "your" : $"Player {order}\'s")} name: ");
            string name = "";
            while (name.Trim() == "")
            {
                name = Console.ReadLine();
            }
            return name;
        }

        public static void PrintTurn(Model game, List<Player> players)
        {   
            if (players[game.CurrentPlayer] is Computer)
            {
                Console.WriteLine($"{players[game.CurrentPlayer].PlayerName} is thinking...It will place its {players[game.CurrentPlayer].PlayerSymbol} soon...");
                Thread.Sleep(2000);
            }
            else
                Console.Write($"It is {players[game.CurrentPlayer].PlayerName}'s turn. Place your {players[game.CurrentPlayer].PlayerSymbol} in a column 1-7: ");    
        }

        public static int PromptHumanColumn()
        {
            int column = 0;
            while (column < 1 || column > 7)
            {
                try
                {
                    column = Convert.ToInt32(Console.ReadLine());
                    if (column < 1 || column > 7) Console.Write("Please enter a number between 1 and 7: ");
                }
                catch
                {
                    Console.Write("Please enter a number between 1 and 7: ");
                }
            }
            return column;
        }

    }
}
