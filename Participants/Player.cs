class Player : Participant
{
    public Player(string name) : base(name) { }

    public int Wins {get; private set;} = 0;
    public int Losses {get; private set;} = 0;
    public int Draws {get; private set;} = 0;


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