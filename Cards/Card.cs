class Card
{
    public Suit Suit {get;}
    public Rank Rank {get;}

    public Card(Suit suit, Rank rank)   //Constructor
    {
        Suit = suit;
        Rank = rank;
    }
}