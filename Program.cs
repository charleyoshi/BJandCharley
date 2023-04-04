using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConnectFour
{
    class UserIO
    {// interacts and communicates via text input and Console output

        // [DONE] StartScreen, PrintBoard
        public static int StartScreen()
        {
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
            // Console.Clear();
            Console.WriteLine();
            for (int i = 0; i < game.Board.GetLength(0); i++)
            {
                Console.Write("|  ");
                for (int j = 0; j < game.Board.GetLength(1); j++)
                {
                    if (game.Board[i, j] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(game.Board[i, j]);
                        Console.ResetColor();
                    }
                    else if (game.Board[i, j] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(game.Board[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(game.Board[i, j]);
                    }

                    Console.Write("  ");
                }
                Console.WriteLine("| ");
            }
            Console.WriteLine("   1  2  3  4  5  6  7  ");
        }
        // Todo:  PrintTurn, PrintWinner, PrintTie
        public static string getName(int order)
        {
            Console.Write($"Enter Player {order}\'s name: ");
            string name = "";
            while (name == "")
            {
                name = Console.ReadLine();
            }
            return name;
        }

        public static void PrintTurn(List<Player> players, Model game)
        {
1            //Console.Write($"It is {players[game.CurrentPlayer].PlayerName}'s turn. Place your {players[game.CurrentPlayer].PlayerSymbol} in a column 1-7: ");
             Console.Write($"It is {players[game.CurrentPlayer].PlayerName}'s turn. Place your {players[game.CurrentPlayer].PlayerSymbol} in a column 1-7: ");

        }


        public static int GetColumn()
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

    abstract class Player
    {

        public int PlayerOrder { get; set; }
        public string PlayerName { get; set; }
        public char PlayerSymbol { get; set; }
        public Player(int order)
        {
            PlayerOrder = order;
            PlayerSymbol = (PlayerOrder == 1) ? 'X' : 'O';
            PlayerName = "Computer"; // Default name
        }

        // Abstract method (does not have a body)
        public abstract void animalSound();
        // Regular method
        public void sleep()
        {
            Console.WriteLine("Zzz");
        }
    }

    class Human : Player
    {
        public Human(int order, string name) : base(order) { PlayerName = name; } //Constructor for Human with Name
        public override void animalSound()
        {
            // The body of animalSound() is provided here
            Console.WriteLine("The pig says: wee wee");
        }
    }

    // class Computer : Player
    // {
    //     // Optional  
    // }

    class Model
    {
        public char[,] Board { get; set; }
        public int CurrentPlayer { get; set; } // Either 0 or 1
        private List<bool> IsColumnFull = new List<bool>();
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
        {   // [Done]
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
        {   // [Done]
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
        {   // [Done] Return true if all columns are full
            return IsColumnFull.All(x => x);
        }

    }


    class Controller
    {
        private Model game;
        private List<Player> players = new List<Player>();
        public Controller() { game = new Model(); } // Constructor
        public void Play()
        {
            // starting and running the game loop:

            // 1. StartScreen,Ask user 1P or 2P
            // 2. while !isGameOver, makeMove
            int NumOfPlayers = UserIO.StartScreen(); // Either 1 or 2
            if (NumOfPlayers == 1) { }
            else if (NumOfPlayers == 2)
            {
                for (int i = 0; i < NumOfPlayers; i++)
                {
                    string userName = UserIO.getName(i + 1);
                    players.Add(new Human(i + 1, userName));
                }
                foreach (var p in players) { Console.WriteLine($"{p.PlayerOrder}: {p.PlayerName} will go by {p.PlayerSymbol}"); }
                Console.WriteLine("When you're ready, press enter to play.");
                Console.ReadLine();
            }

            do  // PrintTurn, MakeMove
            {
                UserIO.PrintBoard(game);
                UserIO.PrintTurn(players, game);
                game.MakeMove(players[game.CurrentPlayer], UserIO.GetColumn());
            } while (!game.IsGameOver());

            // Either Win or Tie
            UserIO.PrintBoard(game);
            if (game.Winner != null) Console.WriteLine($"It is a Connect 4. {players[game.Winner.Value].PlayerName} Wins!"); // someone wins
            else Console.WriteLine("Tie!"); // no one wins but game over: Tie


        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {

            //In either case, when the game ends, 
            // the game should show some text indicating either who won or that the game was a draw. 
            //The game must then return to the "start" screen, 
            //where a player can once again choose either 1-player mode or 2-player mode (Optional).

            while (true)
            {
                Controller controller = new Controller();
                controller.Play();

                Console.Write("Restart? Yes(1) No(0): ");
                Console.ReadKey(); // Todo: implement this line
            }
        }

    }
}









// Problem:
// playerorder has to be reset after every game