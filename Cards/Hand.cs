class Hand
{
    private List<Card> _cards = [];
    public  IReadOnlyList<Card> Cards => _cards;

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }
} 