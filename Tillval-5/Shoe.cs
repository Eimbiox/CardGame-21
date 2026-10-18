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
            foreach (var suit in Enum.GetValues<Suit>())    //Creates 52 cards per deck (amountofdecks = 6)
            {
                foreach (var rank in Enum.GetValues<Rank>())
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
        }
    }
   
}