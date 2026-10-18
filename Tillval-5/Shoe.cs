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
    }
}