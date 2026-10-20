class Deck
{
    private List<Card> _cards = [];
    private Shoe? _shoe;
    public static Deck? Current { get; private set; } //The latest created deck. Lets ConsoleUI read it, since Game never gives it the deck.

    public int RemainingCards
    {
        get
        {
            if (_shoe != null && _shoe.CutCardReached)
            {
                return 0;
            }
            if (_shoe != null)
            {
                return _shoe.RemainingCards;
            }
            return _cards.Count();
        }
    }

    public static bool UseShoe {get; set;}

    public Deck() //Constructor
    {
        Current = this; //This new deck becomes the current one, so ConsoleUI always reads the deck in use
        if (UseShoe == true)
        {
            _shoe = new Shoe();
            return;
        }
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
        if (_shoe != null)
        {
            _shoe.Shuffle();
            return;
        }
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = Random.Shared.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }

    public Card TakeCard()  //Takes the first card
    {
        if(_shoe != null)
        {
            return _shoe.TakeCard();
            
        }
        Card card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }
}