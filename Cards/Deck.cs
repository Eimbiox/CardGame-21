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
    public int CardsLeft => _shoe != null ? _shoe.RemainingCards : _cards.Count(); //Real number of cards left, used by the card counter
    public static bool UseShoe {get; set;}
    public static int NumberOfDecks { get; set; } = 1;    //Antal lekar utan sko. Sätts från rules.json (tillval 3)
    public static int ShoeDecks { get; set; } = 6;        //Antal lekar i skon. Sätts från rules.json (tillval 3)

    public Deck() //Constructor
    {
        Current = this; //This new deck becomes the current one, so ConsoleUI always reads the deck in use
        if (UseShoe == true)
        {
            _shoe = new Shoe(ShoeDecks);
            return;
        }
        for (int i = 0; i < NumberOfDecks; i++)     //En lek med 52 kort för varje lek i rules.json
        {
            foreach (var suit in Enum.GetValues<Suit>())    //Creates 52 cards when deck is made
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