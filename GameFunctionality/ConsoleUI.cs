class ConsoleUI
{
    public int MainMenu()
    {
        Console.Clear();
        GameStart();
        Console.WriteLine();

        Console.WriteLine("1. Starta\n2. Visa topplistan\n3. Sök efter spelares reslutat\n4. Avsluta");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice is < 1 or > 4)
        {
            Console.WriteLine("Ange ett heltal mellan 1 och 4");
        }
        return choice;
    }
    public void GameStart()
    {
        Console.WriteLine("=====================================");
        Console.WriteLine("        Välkommen till Tjugoett!     ");
        Console.WriteLine("=====================================");
    }
    public void ShowLeaderboard(GameStorage gameStorage)    //Visar Topplistan
    {
        Console.Clear();
        //Sorterar spelare i gameStorage._players baserat på deras winRate
        //Om flera spelare har samma winRate sorteras dem efter vinster
        var sortedPlayers = gameStorage._players.OrderByDescending(p => p.WinRate).ThenByDescending(p => p.Wins).ToList();
        Console.WriteLine();
        Console.WriteLine("==================== TOPPLISTA =====================");
        Console.WriteLine($"{"#",-2} {"Namn",-15} {"Vinst",5} {"Förlust",7} {"Lika",4} {"Vinstfrekvens",13}");
        Console.WriteLine(new string('-', 52));
        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            Player player = sortedPlayers[i];
            Console.WriteLine($"{i + 1,-2} {player.Name,-15} {player.Wins,5} {player.Losses,7} {player.Draws,4} {player.WinRate * 100,13:F1}%");
        }
        Console.WriteLine(new string('=', 52));
        Console.ReadKey();
    }

    public void FindPlayerStats(GameStorage gameStorage)    //Hittar en specifik spelare i topplistan
    {
        //Sorterar spelare i gameStorage._players baserat på deras winRate
        //Om flera spelare har samma winRate sorteras dem efter vinster
        var sortedPlayers = gameStorage._players.OrderByDescending(p => p.WinRate).ThenByDescending(p => p.Wins).ToList();
        Console.Write("Skriv namnet på spelaren: ");
        string? nameToFind = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(nameToFind))
        {
            Console.Write("Skriv ett giltigt namn");
            nameToFind = Console.ReadLine();
        }
        bool nameFound = false;
        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            Player player = sortedPlayers[i];
            if (player.Name == nameToFind)
            {
                Console.Clear();
                Console.WriteLine($"{"#",-2} {"Namn",-15} {"Vinst",5} {"Förlust",7} {"Lika",4} {"Vinstfrekvens",13}");
                Console.WriteLine(new string('-', 52));
                Console.WriteLine($"{i + 1,-2} {player.Name,-15} {player.Wins,5} {player.Losses,7} {player.Draws,4} {player.WinRate * 100,13:F1}%");
                Console.WriteLine(new string('-', 52));
                nameFound = true;
            }
        }
        if (!nameFound)
        {
            Console.WriteLine($"{nameToFind} finns inte i topplistan");
        }
        Console.ReadKey();
    }

    public int AskNumberOfPlayers()
    {
        while (true)
        {
            Console.Write("Ange antal spelare (1-5): ");
            if (int.TryParse(Console.ReadLine(), out int count) && count is >= 1 and <= 5)
                return count;
            Console.WriteLine("Ange ett heltal mellan 1 och 5.");
        }
    }
    public string AskPlayerName(int playerNumber)
    {
        Console.Write($"Skriv namnet på spelare {playerNumber}: ");
        string? name = Console.ReadLine()?.Trim();
        while (string.IsNullOrWhiteSpace(name))
        {
            Console.Write("Namnet får inte vara tomt: ");
            name = Console.ReadLine()?.Trim();
        }
        return name;
    }
    public bool AskIfPlayAgain()
    {
        while (true)
        {
            Console.Write("Vill du fortsätta spela?(Ja/Nej): ");
            string? answer = Console.ReadLine()?.ToUpper().Trim();
            if (!string.IsNullOrWhiteSpace(answer))
            {
                if (answer == "JA")
                {
                    return true;
                }
                if (answer == "NEJ")
                {
                    return false;
                }
                Console.WriteLine("Svara med ja eller nej!");
            }
            else
            {
                Console.WriteLine("Det får inte vara tomt!");
            }
        }
    }

    public bool WantsToHit(Player player) //true = hit false = stand
    {
        while (true)
        {
            Console.Write($"{player.Name}, Köp eller Stanna?: ");
            string? input = Console.ReadLine()?.ToUpper().Trim();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input == "KÖP")
                {
                    return true;
                }
                if (input == "STANNA")
                {
                    return false;
                }
                Console.WriteLine("Svara med köp eller stanna");
            }
            else
            {
                Console.WriteLine("Det får inte vara tomt");
            }
        }

    }
    public void ShowOutcome(Player player, Outcome outcome)
    {
        if (outcome == Outcome.Lose && player.Hand.IsBust)
        {
            Console.WriteLine($"{player.Name} spräckte med {player.Hand.Points} och förlorar.");
        }
        else if (outcome == Outcome.Win)
        {
            Console.WriteLine($"{player.Name} vinner!");
        }
        else if (outcome == Outcome.Lose)
        {
            Console.WriteLine($"{player.Name} förlorar.");
        }
        else if (outcome == Outcome.Push)
        {
            Console.WriteLine($"{player.Name} och givaren har lika.");
        }
        else if (outcome == Outcome.Blackjack) // Lade till en ny outcome för blackjack, då det är en speciell vinst.
        {
            Console.WriteLine($"{player.Name} har BLACKJACK och vinner!");
        }

    }

    public void DealerTurnMessage()
    {
        Console.WriteLine("----Givarens tur----");
    }

    public void ShowBlackjack(Participant participant)
    {
        Console.WriteLine($"{participant.Name} fick BLACKJACK!");
    }


    public void ShowHand(Participant participant) //Visar korten, värdet om handen är mjuk eller hård.
    {
        //Ett kort ritas på tre rader. Då jag bygger varje rad för sig med, Top,Middle och Bottom Layer.
        string top = ""; //Översta raden med "┌───┐" 
        //string middle = ""; //Mittenraden med " │" + CardText.PadRight(3) + "│ 
        string bottom = ""; // Nedersta raden "└───┘"


        foreach (Card card in participant.Hand.Cards) //Går igenom vaje kort som ligger i handen, ett i taget.

        {
            //Gör om kortets enums för Rank + Suits till symboler som "♥♦♣♠" döpt lådan till "CardText"
            // string CardText = RankSymbol(card.Rank) + SuitSymbol(card.Suit);

            top += "┌───┐ ";
            // middle += "│" + CardText.PadRight(3) + "│ "; //Anropa "CardText" + lagt till PadRight så den alltid blir 3 rader
            bottom += "└───┘ "; //T.ex 1♠ då denna är 2 rader lägger till "/n" och 10♠ är 3 rader.
        }


        string handType; //Text som visar om handen är "mjuk" eller "hård"

        if (participant.Hand.IsSoft) //Om ett ess fortfrande räknas som 11

        {
            handType = "mjuk"; //Handen är mjuk
        }
        else //Inget ess räknas som 11
        {
            handType = "hård"; //Handen är hård.
        }

        //Skriver ut namnet på spelaren,            Vad för kort, Mjuk/Hard  Poängen som man har totalt i handen.
        Console.WriteLine($"{participant.Name}: ({handType} hand, {participant.Hand.Points})\n");
        Console.WriteLine(top);


        foreach (Card card in participant.Hand.Cards) //Mittenraden skrivs kort för kort så att varje kort kan få egen färg.
        {
            Console.Write("│"); //Vänster kant på kortet
            WriteCardText(card); //Kortets  text, röd om hjärter/ruter

            Console.Write("│ "); //Högerkant på kortet
        }
        Console.WriteLine(); // Ny rad efter alla kort


        //  Console.WriteLine(middle);
        Console.WriteLine(bottom);
        Console.WriteLine(); //Tom Rad för med luftigt i Konsolen.

        if (participant.Hand.IsBust) //IsBust är true om poängen är över 21.
        {
            Console.WriteLine($"{participant.Name} spräckte!\n");
        }

    }

    public void ShowDealerHand(Dealer dealer)  //Visar givarens hand. SÅlänge hålkortet är dolt visas det med baksidan upp som │░░░│ 
    {
        if (dealer.IsHoleCardHidden) //Om hålkortet fortfarande är dolt ("1♠, hållkort")
        {
            Card upcard = dealer.Hand.Cards[0];  //Första kortet i listan inom index som visas medans andra andra är hålkortet
           // string text = RankSymbol(upcard.Rank) + SuitSymbol(upcard.Suit); //T.ex. "1♦"


            Console.WriteLine($"{dealer.Name}");
            Console.WriteLine("┌───┐ ┌───┐");



            // Console.WriteLine("│" + text.PadRight(3) + "│ │░░░│"); //Det öppna kortet och hålkortet med baksidan upp. [0][1]
            Console.Write("│"); //vänster kant på kortet
            WriteCardText(upcard);         //Givarens öppna kort, rött om hjärter/ruter
            Console.WriteLine("│ │░░░│"); //höger kant på kortet





            Console.WriteLine("└───┘ └───┘");
            Console.WriteLine();

        }
        else //Hålkortet är vänt, visa hela dealerns hand
        {
            ShowHand(dealer); //kalla på funktionen showhand.
        }
    }
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
    private void WriteCardText(Card card) // Skruver ut ett kort som text, t.ex "Q♦". 

    {
        string cardText = RankSymbol(card.Rank) + SuitSymbol(card.Suit); //Gör om kortet till text, t.ex "Q" + "♦" = "Q♦"

        if (card.Suit == Suit.Hearts || card.Suit == Suit.Diamonds) //Om kortet är hjärter eller ruter?

        {
            Console.ForegroundColor = ConsoleColor.Red; //Sätter nuvarande textfärg till röd, så  att kortet skrivs ut i rött
        }

        Console.Write(cardText.PadRight(3)); //Skriver ut kortet alltså "Q♦", PadRight(3) ger alltid texten 3 tecken "Q♦ " lägger till /n
        Console.ResetColor(); //Byt tillbaka till vanlig färg, annars blir all text efteråt röd.


    }


}