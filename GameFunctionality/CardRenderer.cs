class CardRenderer

{
    private string RankSymbol(Rank rank) //Gör om enum till text, t.ex Rank.Ace blir A

    {
        switch (rank)
        {
            case Rank.Ace: return "A";
            case Rank.King: return "K";
            case Rank.Queen: return "Q";
            case Rank.Jack: return "J";
            case Rank.Ten: return "10";
            case Rank.Nine: return "9";
            case Rank.Eight: return "8";
            case Rank.Seven: return "7";
            case Rank.Six: return "6";
            case Rank.Five: return "5";
            case Rank.Four: return "4";
            case Rank.Three: return "3";
            default: return "2"; //Det enda som är kvar är Two

        }
    }

    private string SuitSymbol(Suit suit) //Gör om färg enum till symbol där Suit.Hearts blir ♥

    {
        switch (suit)
        {
            case Suit.Hearts: return "♥";
            case Suit.Diamonds: return "♦";
            case Suit.Spades: return "♠";
            default: return "♣"; // Det enda är kvar är Klöver

        }
    }

    private string[] GetCardLine(Card card)

    {
        string rank = RankSymbol(card.Rank); // Gör om t.ex Rank.Two till 2 o döper ny Textlåda till rank
        string suit = SuitSymbol(card.Suit); // Gör om t.ex. Suit.Hearts till "♥" och sparar i en ny textlåda som heter suit

        string[] lines = new string[7]; //Skapa fler lådor med fack som heter lines, då detär 7st.

        lines[0] = "┌───────┐";
        lines[1] = "│" + rank.PadRight(7) + "│"; //Lägger till Rank 2 t.ex plus totalt 7 mellanslag
        lines[2] = "│       │";
        lines[3] = "│   " + suit + "   │"; //lägger till suit
        lines[4] = "│       │";
        lines[5] = "│" + rank.PadLeft(7) + "│";
        lines[6] = "└───────┘"; // // Skickar tillbaka kortets 7 rader till DrawCards (syns inte på skärmen förrän DrawCards skriver ut dem)

        return lines;

    }

    private string [] GetBackLines() //Bygger baksidan av kortet.

    {
        string [] lines = new string [7];

        lines[0] = "┌───────┐";
        lines[1] = "│░░░░░░░│";
        lines[2] = "│░░░░░░░│";
        lines[3] = "│░░░░░░░│";
        lines[4] = "│░░░░░░░│";
        lines[5] = "│░░░░░░░│";
        lines[6] = "└───────┘";

        return lines;
        
    }



}
