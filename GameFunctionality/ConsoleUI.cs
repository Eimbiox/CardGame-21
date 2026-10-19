class ConsoleUI
{
    private readonly CardRenderer _cardRenderer = new CardRenderer();   //Ritar korten i konsolen
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

    public void PrintErrors(GameStorage gameStorage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(string.Join("\n", gameStorage.Errors));
        Console.ResetColor();
        Console.ReadKey();
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
    public string AskPlayerName(string promt)
    {
        Console.Write(promt);
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

        string handType; //Text som visar om handen är "mjuk" eller "hård"

        if (participant.Hand.IsSoft) //Om ett ess fortfrande räknas som 11

        {
            handType = "mjuk"; //Handen är mjuk
        }
        else //Inget ess räknas som 11
        {
            handType = "hård"; //Handen är hård.
        }

        Console.WriteLine($"{participant.Name}: ({handType} hand, {participant.Hand.Points})\n");
        _cardRenderer.DrawCards(participant.Hand.Cards);   //Ritar alla kort med framsidan upp

        if (participant.Hand.IsBust)   //IsBust är true om poängen är över 21
        {
            Console.WriteLine($"{participant.Name} spräckte!\n");
        }
    }

    public void ShowDealerHand(Dealer dealer)   //Visar givarens hand. Så länge hålkortet är dolt ritas det med baksidan upp
    {
        if (dealer.IsHoleCardHidden)   //Är hålkortet fortfarande dolt?
        {
            Console.WriteLine(dealer.Name);
            _cardRenderer.DrawCards(dealer.Hand.Cards, true);   //true = rita andra kortet med baksidan upp
        }
        else   //Hålkortet är vänt, visa hela handen
        {
            ShowHand(dealer); //Visa dealernskort
        }
    }
}