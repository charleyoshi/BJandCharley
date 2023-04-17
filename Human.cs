namespace ConnectFour
{
    class Human : Player
    {
        public Human(int order, string name) : base(order)
        {
            PlayerName = name;
        }

        public override int GetColumn()
        {
            return UserIO.PromptHumanColumn();
        }
    }
}
