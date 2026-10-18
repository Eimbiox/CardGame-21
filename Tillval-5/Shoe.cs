// Multiple Decks are shuffled into one.
// A cut card is placed at random position in the last quarter
// and marks when the shoe should be reshuffled
class Shoe
{

    public int AmountOfDecks {get; private set;}


    public bool CutCardReached {get; private set;}

    public int RemainingCards => _cards.Count;

    private int _cutCard;

    private List<Card> _cards = [];

    public Shoe(int amountofdecks = 6)
    {
        if (amountofdecks < 2)
        {
            amountofdecks = 2; // Minimum 2 decks, otherwise the cut card won't fit.
                               // Example with 1 deck: from 52 * 3/4 = 39, to 52 - 15 = 37.
                               // "from" is bigger than "to", which would crash Random.Next.
        }
        AmountOfDecks = amountofdecks;
        for(int i = 0; i < amountofdecks; i++)
        {
            foreach (var suit in Enum.GetValues<Suit>())    //Creates 52 cards per deck (amountofdecks = 6) 312 total cards.
            {
                foreach (var rank in Enum.GetValues<Rank>())
                {
                    _cards.Add(new Card(suit, rank));
                }
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
        _cutCard = Random.Shared.Next(RemainingCards * 3 / 4, RemainingCards - 15); // Instead of having (234, 312) I added the current one for different amountofdecks values
    }

    public Card TakeCard()  //Takes the first card
    {
        Card card = _cards[0];
        _cards.RemoveAt(0);
        int drawnCards = AmountOfDecks * 52 - RemainingCards;
        if(drawnCards >= _cutCard)
        {
            CutCardReached = true;
        }
        return card;
    }
}