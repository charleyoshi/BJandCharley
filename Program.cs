using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConnectFour
{
    class UserIO
    {// interacts and communicates via text input and Console output
        public static int StartScreen()
        {
            int playerCount = 0;

            Console.WriteLine("Welcome to Connect Four! 1Player or 2Player?");
            while (playerCount != 1 && playerCount != 2)
            {
                try
                {
                    playerCount = Convert.ToInt32(Console.ReadLine());
                    if (playerCount != 1 && playerCount != 2) Console.WriteLine("Please enter 1 or 2");
                }
                catch
                {
                    Console.WriteLine("Please enter 1 or 2");
                }
            }
            return playerCount;
        }

        // Todo: StartScreen, PrintTurn, PrintWinner, PrintTie, 
        public static void PrintBoard(Model game)
        {
            Console.Clear();
            Console.WriteLine();
            for (int i = 0; i < game.Board.GetLength(0); i++)
            {
                Console.Write("|  ");
                for (int k = 0; k < game.Board.GetLength(1); k++)
                {
                    Console.Write(game.Board[i, k]);
                    Console.Write("  ");
                }
                Console.WriteLine("| ");
            }
            Console.WriteLine("   1  2  3  4  5  6  7  ");
        }
    }

    abstract class Player
    {
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
        public Model()
        {
            int row = 6;
            int column = 7;
            Board = new char[row, column]; // # for empty, X for red, O for yellow
            // Fill up the board with #
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Board[i, j] = '#';
                }
            }
        }
        public bool MakeMove(int column, int player)
        {
            // TODO: implement the logic for placing a disc in a column and checking for a win
            return true;
        }

        public bool IsGameOver()
        {
            // TODO: implement the logic for checking if the game is over (either a win or a tie)
            return true;
        }

    }


    class Controller
    {
        private Model game;
        public Controller() { game = new Model(); } // Constructor

        public void Play()
        {
            // TODO: implement the logic for starting and running the game loop

            // 1. StartScreen,Ask user 1P or 2P
            // 2. while !isGameOver, makeMove

            int NumOfPlayers = UserIO.StartScreen(); // Either 1 or 2
            if (NumOfPlayers == 1) { }
            else if (NumOfPlayers == 2)
            {
                // TODO: Add 2 Human Players
                UserIO.PrintBoard(game);
            }


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

                Console.WriteLine("Press any key to play again");
                Console.ReadKey();
            }
        }

    }
}







