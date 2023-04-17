namespace ConnectFour
{
    class Computer : Player
    {
        static Random random = new Random();
        public Computer(int order) : base(order) { }
        public override int GetColumn()
        {
            // Probability for [Column]:    40%[4]   30%[3 or 5]    20%[2 or 6]   10%[1 or 7]
            // Cumulative Probability:      40%      70%            90%           100%
            double probability = random.NextDouble();
            if (probability < 0.4)
                return 4;
            else if (probability < 0.7)
                return random.Next(0, 2) == 0 ? 3 : 5;
            else if (probability < 0.9)
                return random.Next(0, 2) == 0 ? 2 : 6;
            else
                return random.Next(0, 2) == 0 ? 1 : 7;
        }
    }
}
