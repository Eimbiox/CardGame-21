using System.Drawing;

class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }
    public int Point { get; }

    public Card(Suit suit, Rank rank)   //Constructor
    {
        Suit = suit;
        Rank = rank;


        if (Rank == Rank.Ace)
        {
            Point = 11; 
        }
        else if (Rank == Rank.Jack || Rank == Rank.Queen || Rank == Rank.King)
        {
            Point = 10;
        }
        else if (Rank == Rank.Two) { Point = 2; }
        
        else if (Rank == Rank.Three) { Point = 3; }
        
        else if (Rank == Rank.Four) { Point = 4; }
        
        else if (Rank == Rank.Five) { Point = 5; }

        else if (Rank == Rank.Six) { Point = 6; }

        else if (Rank == Rank.Seven) { Point = 7; }

        else if (Rank == Rank.Eight) { Point = 8; }

        else if (Rank == Rank.Nine) { Point = 9; }

        else

        {
            Point = 10;
        }
        }

    }

