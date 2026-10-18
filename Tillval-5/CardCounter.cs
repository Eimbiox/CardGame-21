// Card counter using Hi-Lo system.
class CardCounter
{
    public int RunningCount {get; private set;}

    public void ObserveCard(Card card)
    {
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
}
