namespace ConnectFour
{
    class Controller
    {
        private Model game;
        private List<Player> players = new List<Player>();
        public Controller() { game = new Model(); } // Constructor
        public void Play()
        {
            // Starting and running the game loop:

            // 1. StartScreen,Ask user 1P or 2P
            // 2. while !isGameOver, makeMove
            int NumOfPlayers = UserIO.StartScreen(); // Either 1 or 2
            if (NumOfPlayers == 1)
            {
                string userName = UserIO.getName();
                Random random = new Random();
                int firstPlayer = random.Next(1, 3);
                Console.WriteLine($"Randomly deciding who goes first... {(firstPlayer == 1 ? "Human" : "Computer")} will go first.");
                players.Add(firstPlayer == 1 ? new Human(1, userName) : new Computer(1));
                players.Add(firstPlayer == 1 ? new Computer(2) : new Human(2, userName));
            }
            else if (NumOfPlayers == 2)
            {
                for (int i = 0; i < NumOfPlayers; i++)
                {
                    string userName = UserIO.getName(i + 1);
                    players.Add(new Human(i + 1, userName));
                }
            }
            foreach (var p in players) { Console.WriteLine($"{p.PlayerOrder}: {p.PlayerName} will go by {p.PlayerSymbol}"); }
            Console.WriteLine("When you're ready, press enter to play.");
            Console.ReadLine();

            do  // PrintTurn, MakeMove
            {
                UserIO.PrintBoard(game, players);
                UserIO.PrintTurn(game, players);
                game.MakeMove(players[game.CurrentPlayer], players[game.CurrentPlayer].GetColumn());
            } while (!game.IsGameOver());

            // Either Win or Tie
            UserIO.PrintBoard(game);
            if (game.Winner != null) UserIO.PrintWinner(players[game.Winner.Value]); //Console.WriteLine($"It is a Connect 4. {players[game.Winner.Value].PlayerName} Wins!"); // someone wins
            else UserIO.PrintTie(); // no one wins but game over: Tie
        }
    }
}
