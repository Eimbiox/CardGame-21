class Player : Participant
{
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Draws { get; private set; } 

    public Wallet Wallet  {get; }= new Wallet();
    public double WinRate => CalculateWinRate();
    
    public Player(string name, int wins = 0, int losses = 0, int draws = 0) : base(name)    //Konstruktor
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

    public double CalculateWinRate()
    {
        int gamesPlayed = Wins + Losses + Draws;
        
        if (gamesPlayed == 0)
        {
            return 0;
        }
        return (double)Wins / (double)gamesPlayed;
    }
}