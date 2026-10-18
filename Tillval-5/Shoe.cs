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
        _cutCard = Random.Shared.Next(RemainingCards * 3 / 4, RemainingCards); // Instead of having (234, 312) I added the current one for different amountofdecks values
    }
}