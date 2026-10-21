class Player : Participant
{
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Draws { get; private set; } 

    public Wallet Wallet  {get; }
    public double WinRate => CalculateWinRate();
    
    //Hur spelaren fattar beslut. Sätts i Game när spelaren sätter sig vid bordet, behöver vara public 
    public IPlayerStrategy Strategy { get; set; } = null!;

    public bool IsComputer => Strategy.IsComputer;   //Frågar strategin, så att Round inte behöver känna till strategiklasserna

    public Move Decide(Card dealerUpcard)   //Frågar strategin vad spelaren ska göra med sin hand
    {
        return Strategy.ChooseMove(Hand, dealerUpcard);
    }

    public Player(string name, int wins = 0, int losses = 0, int draws = 0, int chips = Wallet.StartingChips) : base(name)  //Konstruktor
    {
        Wins = wins;
        Losses = losses;
        Draws = draws;
        Wallet = new Wallet(chips);
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