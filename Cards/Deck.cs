class Deck
{
    private List<Card> _cards = [];

    public Deck() //Constructor
    {
        foreach (var suit in Enum.GetValues<Suit>())    //Creates 52 cards when deck is made
        {
            foreach (var rank in Enum.GetValues<Rank>())
            {
                _cards.Add(new Card(suit, rank));
            }
        }
    }
    
    public void Shuffle()   //Shuffles the cards. 
    {
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = Random.Shared.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }

    public Card TakeCard()  //Takes the first card
    {
        return _cards[0];
    }
}