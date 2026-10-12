class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card(Suit suit, Rank rank)   //Constructor
    {
        Suit = suit;
        Rank = rank;
    }



    public int GetPoint() // En metod som returerna hur mkt poängen korten är värt.

    {
        if (Rank == Rank.Ace) //Returnera 11 points

        {
            return 11; // Måste hitta någon metod som gör 11 om till 1 om man blir busted.
        }
        if (Rank == Rank.Jack || Rank == Rank.Queen || Rank == Rank.King) //Returnera 10 points

        {
            return 10;
        }
        //Behöver lägga till värde från 2 till 9
        if (Rank == Rank.Two)
        {
            return 2;
        }
        if (Rank == Rank.Three)
        {
            return 3;
        }
        if (Rank == Rank.Four)
        {
            return 4;
        }
        if (Rank == Rank.Five)
        {
            return 5;
        }
        if (Rank == Rank.Six)
        {
            return 6;
        }
        if (Rank == Rank.Seven)
        {
            return 7;
        }
        if (Rank == Rank.Eight)
        {
            return 8;
        }
        if (Rank == Rank.Nine)
        {
            return 9;
        }
        else
        {
            return 10;

        }

    }
}
