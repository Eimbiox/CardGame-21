// Card counter using Hi-Lo system.
class CardCounter
{
    public int RunningCount {get; private set;}
    private List<Card> _seenCards = [];


    public void ObserveCard(Card card)
    {
        if (_seenCards.Contains(card))
    {
        return; // Already counted, do nothing
    }
    _seenCards.Add(card);


        if(card.Point >1 && card.Point < 7)
        {
            RunningCount = RunningCount + 1;
        }
        else if(card.Point > 6 && card.Point < 10)
        {
            //No value gets added to the RunningCount
        }
        else if(card.Point > 9 && card.Point < 12)
        {
            RunningCount = RunningCount -1;
        }
    }

    public void ResetRunningCount()
    {
        RunningCount = 0;
        _seenCards.Clear();
    }


    // Running count säger hur många fler höga kort som finns kvar. True count säger hur tätt de ligger, och det är det som avgör om läget är bra.
    public double TrueCount(int RemainingCards)
    {
        double decksLeft = RemainingCards / 52.0;
        double trueCount = RunningCount / decksLeft;
        return trueCount;
    }

    public CountAdvantage FavorableLevel(int RemainingCards)
    {
        double trueCountValue = TrueCount(RemainingCards);
        if(trueCountValue >= 2)
        {
            return CountAdvantage.Favorable;
        }
        else if(trueCountValue > 1)
        {
            return CountAdvantage.Neutral;
        }
        else
        {
            return CountAdvantage.Unfavorable;
        }
    }
}
