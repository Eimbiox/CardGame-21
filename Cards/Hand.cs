class Hand
{
    private List<Card> _cards = [];
    public  IReadOnlyList<Card> Cards => _cards;
    public int Points {get; private set;}
    public void AddCard(Card card)
    {
        _cards.Add(card);
    }
    public void ClearHand()
    {
        _cards.Clear();
    }
    public void CountPoints()
    {
        //Räkna ihop korternas värde. (ess ska vara 1 or 11)
    }
} 