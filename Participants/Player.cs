class Player : Participant
{
    public int Wins { get; private set; } = 0;
    public int Losses { get; private set; } = 0;
    public int Draws { get; private set; } = 0;
    
    // public Player(string name) : base(name) { } //Constructor

    public Player(string name, int wins = 0, int losses = 0, int draws = 0) : base(name)
    {
        Wins = wins;
        Losses = losses;
        Draws = draws;
    }

    public void AddResult(Outcome outcome)
    {
        if (outcome == Outcome.Win)
        {
            Wins += 1;
        }
        else if (outcome == Outcome.Lose)
        {
            Losses += 1;
        }
        else if (outcome == Outcome.Push)
        {
            Draws += 1;
        }
        else    //Blackjack
        {
            Wins += 1;
        }
    }
}