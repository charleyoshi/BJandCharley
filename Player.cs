namespace ConnectFour
{
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

        public abstract int GetColumn();
    }
}
