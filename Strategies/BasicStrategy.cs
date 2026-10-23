class BasicStrategy : IPlayerStrategy   // Använder grunderna för 21 med hjälp av hårdkodade tabeller
{
    private const Move H = Move.Hit;
    private const Move S = Move.Stand;

    public bool IsComputer => true;

    private static readonly Move[,] HardTable =
    {
        //         2  3  4  5  6  7  8  9  10 A
        /*  8 */ { H, H, H, H, H, H, H, H, H, H },
        /*  9 */ { H, H, H, H, H, H, H, H, H, H },
        /* 10 */ { H, H, H, H, H, H, H, H, H, H },
        /* 11 */ { H, H, H, H, H, H, H, H, H, H },
        /* 12 */ { H, H, S, S, S, H, H, H, H, H },
        /* 13 */ { S, S, S, S, S, H, H, H, H, H },
        /* 14 */ { S, S, S, S, S, H, H, H, H, H },
        /* 15 */ { S, S, S, S, S, H, H, H, H, H },
        /* 16 */ { S, S, S, S, S, H, H, H, H, H },
        /* 17 */ { S, S, S, S, S, S, S, S, S, S },
    };

    //Rader: spelarens mjuka summa 13–20 (A+2 till A+9)
    private static readonly Move[,] SoftTable =
    {
        //         2  3  4  5  6  7  8  9  10 A
        /* 13 */ { H, H, H, H, H, H, H, H, H, H },
        /* 14 */ { H, H, H, H, H, H, H, H, H, H },
        /* 15 */ { H, H, H, H, H, H, H, H, H, H },
        /* 16 */ { H, H, H, H, H, H, H, H, H, H },
        /* 17 */ { H, H, H, H, H, H, H, H, H, H },
        /* 18 */ { S, S, S, S, S, S, S, H, H, H },
        /* 19 */ { S, S, S, S, S, S, S, S, S, S },
        /* 20 */ { S, S, S, S, S, S, S, S, S, S },
    };
    public Move ChooseMove(Hand hand, Card dealerUpcard)
    {
       int column = dealerUpcard.Point - 2;   //2 → 0 ... 10 → 8, Ess (11) → 9

       if (hand.IsSoft)
        {
            int row = Math.Clamp(hand.Points, 13, 20) - 13;  //Mjuk 12 (A+A) räknas som mjuk 13 = köp
            return SoftTable[row, column];
        }

        int hardRow = Math.Clamp(hand.Points, 8, 17) - 8;    //Under 8 köper alltid, över 17 stannar alltid
        return HardTable[hardRow, column];
    }
}