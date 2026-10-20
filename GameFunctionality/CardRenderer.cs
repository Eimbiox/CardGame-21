class CardRenderer

{
   public const int CardHeight = 7;  // Hur många rader ett kort är. Används på flera ställen, så det räcker att ändra här. const gör att värdet inte kan ändras medan programmet kör.
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

    private string FaceSymbol(Rank rank) //Schacksymbol för klädda kort (kung, dam, knekt), mellanslag för övrigt.

    {
        switch (rank)

        {
            case Rank.King: return "♚";
            case Rank.Queen: return "♛";
            case Rank.Jack: return "♞";
            default: return " ";
        }
    }
    public string[] GetCardLine(Card card)

    {
        string rank = RankSymbol(card.Rank); // Gör om t.ex Rank.Two till 2 o döper ny Textlåda till rank
        string suit = SuitSymbol(card.Suit); // Gör om t.ex. Suit.Hearts till "♥" och sparar i en ny textlåda som heter suit

        string[] lines = new string[CardHeight]; //Skapa fler lådor med fack som heter lines, då detär 7st.

        lines[0] = "┌───────┐";
        lines[1] = "│" + rank.PadRight(7) + "│"; //Lägger till Rank 2 t.ex plus totalt 7 mellanslag
        lines[2] = "│   " + FaceSymbol(card.Rank) + "   │"; //Schacksymbol för klädda kort, annars tom rad
        lines[3] = "│   " + suit + "   │"; //lägger till suit
        lines[4] = "│       │";
        lines[5] = "│" + rank.PadLeft(7) + "│";
        lines[6] = "└───────┘"; //Skickar tillbaka kortets 7 rader till DrawCards (syns inte på skärmen förrän DrawCards skriver ut dem)

        return lines;

    }

    public string[] GetBackLines() //Bygger baksidan av kortet.

    {
        string[] lines = new string[CardHeight];

        lines[0] = "┌───────┐";
        lines[1] = "│░░░░░░░│";
        lines[2] = "│░░░░░░░│";
        lines[3] = "│░░░░░░░│";
        lines[4] = "│░░░░░░░│";
        lines[5] = "│░░░░░░░│";
        lines[6] = "└───────┘";

        return lines;

    }


    ////Ritar alla kort i en hand bredvid varandra (används för både spelare och givaren).
    //cards = korten som ska ritas, t.ex. spelarens Hand.Cards.
    //hideSecondCard = true ritar andra kortet med baksidan upp (givarens hålkort). Blir false om inget skickas in. 
    public void DrawCards(List<Card> cards, bool hideSecondCard = false)
    {
        for (int row = 0; row < CardHeight; row++) //Går igenom kortets 7 rader uppifrån och ner. För varje rad skrivs samma rad ut för alla kort, så att korten hamnar bredvid varandra

        {
            for (int i = 0; i < cards.Count; i++) //Går genom kortet från vänster till höger, de som faktiskt delar ut.
            {

                bool isHidden = false;              //Vi utgår från att kortet inte är dolt (ändras till true om det är givarens hålkort)
                if (hideSecondCard && i == 1)       //  //Om lappen säger "dölj" OCH det är andra kortet i handen (i = 1), alltså givarens hålkort
                {
                    isHidden = true;                // ...då är det dolt
                }


                bool isRed = false;   //Vi utgår från att kortet inte är rött

                if (cards[i].Suit == Suit.Hearts || cards[i].Suit == Suit.Diamonds)   //Är kortet hjärter ELLER ruter?
                {
                    isRed = true;   //Ja, då är kortet rött och ska ritas i röd färg
                }


                string[] lines;  //Ska innehålla det här kortets 7 rader
                if (isHidden)  //Om det är givarens dolda hålkort

                {
                    lines = GetBackLines(); //...bygg baksidan (░░░)
                }
                else  //Annars är det ett vanligt kort som ska visas med framsidan upp
                {
                    lines = GetCardLine(cards[i]);  //Bygger kortets framsida (valör och färg) och lägger raderna i lines
                }

                if (isRed && !isHidden) //Om kortet är rött (hjärter/ruter) OCH inte dolt...

                {
                    Console.ForegroundColor = ConsoleColor.Red;  //byt textfärg till röd
                }


                Console.Write(lines[row] + " "); //Skriver ut rad nummer "row" av kort nummer "i" + ett mellanslag till nästa kort som hamnar brevid.  
                Console.ResetColor();  //Tillbaka till vanlig färg, så att nästa kort inte också blir rött
            }

            Console.WriteLine(); //Hoppa ner till nästa rad, så att nästa "row" kan skrivas ut för alla kort
        }
        Console.WriteLine(); //Efter att loopen är klar lämnar jag en tom rad, så att det blir luft efter korten



    }
}