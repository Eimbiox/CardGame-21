class ConsoleUI
{
    private CardCounter _counter = new CardCounter();
    private Deck? _lastDeck;
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
               WriteError("Ange ett heltal mellan 1 och 4");
        }
        return choice;
    }

    public void ShowNoPlayersLeft()
    {
        Console.WriteLine("Spelet tog slut för det inte finns några spelare kvar");
        Console.ReadKey();
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

    public void AskUserShoe()
    {
        while (true)
        {
            Console.WriteLine("Vill du spela med sko? (JA/NEJ)");
            string? shoeChoice = Console.ReadLine()?.ToUpper().Trim();
            if(int.TryParse(shoeChoice, out _))
            {
                WriteError("Du kan inte Skriva en siffra");
            }
            else if (string.IsNullOrWhiteSpace(shoeChoice))
            {
                WriteError("Du måste skriva något");
            }
            else if(shoeChoice == "JA")
            {
                Deck.UseShoe = true;
                Console.WriteLine("\n---Sko Används---\n");
                return;
            }
            else if(shoeChoice == "NEJ")
            {
                Deck.UseShoe = false;
                Console.WriteLine("\n---Sko används inte---\n");
                return;
            }
            else
            {
                WriteError("Något gick fel");
            }
        }
    }
    private List<Player> SortPlayers(GameStorage gameStorage)
    {
        //Sorterar spelare i gameStorage._players baserat på deras Chips
        //Om flera spelare har samma Chips sorteras dem efter winRate och sedan wins
        return gameStorage._players.OrderByDescending(p => p.Wallet.Chips).
            ThenByDescending(p => p.WinRate).
            ThenByDescending(p => p.Wins).
            ToList();
    }
    public void ShowLeaderboard(GameStorage gameStorage)    //Visar Topplistan
    {
        Console.Clear();
        
        var sortedPlayers = SortPlayers(gameStorage);
        Console.WriteLine();
        Console.WriteLine("======================= TOPPLISTA =========================");
        Console.WriteLine($"{"#",-2} {"Namn",-15} {"Vinst",5} {"Förlust",7} {"Lika",4} {"Vinstfrekvens",13} {"Marker", 6}");
        Console.WriteLine(new string('-', 59));
        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            Player player = sortedPlayers[i];
            Console.WriteLine($"{i + 1,-2} {player.Name,-15} {player.Wins,5} {player.Losses,7} {player.Draws,4} {player.WinRate * 100,13:F1}% {player.Wallet.Chips, 6}");
        }
        Console.WriteLine(new string('=', 59));
        Console.ReadKey();
    }

    public void FindPlayerStats(GameStorage gameStorage)    //Hittar en specifik spelare i topplistan
    {
        
        var sortedPlayers = SortPlayers(gameStorage);
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
                Console.WriteLine($"{"#",-2} {"Namn",-15} {"Vinst",5} {"Förlust",7} {"Lika",4} {"Vinstfrekvens",13} {"Marker", 6}");
                Console.WriteLine(new string('-', 59));
                Console.WriteLine($"{i + 1,-2} {player.Name,-15} {player.Wins,5} {player.Losses,7} {player.Draws,4} {player.WinRate * 100,13:F1}% {player.Wallet.Chips, 6}");
                Console.WriteLine(new string('-', 59));
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
    public bool AskIsHuman(string prompt)
    {
        Console.Write(prompt); 
        while (true)
        {
            string? answer = Console.ReadLine()?.ToUpper().Trim();
            if (!string.IsNullOrWhiteSpace(answer))
            {
                if (answer == "MÄNNISKA")
                {
                    return true;
                }
                if (answer == "DATOR")
                {
                    return false;
                }
                Console.WriteLine("Ange (Människa/Dator): ");
            }
            else
            {
                Console.WriteLine("Det får inte vara tomt!");
            }
        }
    }

    public string AskWhatBot(string prompt)
    {
        Console.Write(prompt); 
        while (true)
        {
            string? answer = Console.ReadLine()?.ToUpper().Trim();
            if (!string.IsNullOrWhiteSpace(answer))
            {
                if (answer == "SIMPEL")
                {
                    return "Simpel";
                }
                if (answer == "GRUND")
                {
                    return "Grund";
                }
                Console.WriteLine("Ange (Simpel/Grund): ");
            }
            else
            {
                Console.WriteLine("Det får inte vara tomt!");
            }
        }
    }
    public void ShowComputerMove(Player player, Move move)  //Pausar så man hinner se handen, visar beslutet och pausar igen
    {
        Thread.Sleep(1500);   //Tid att se handen innan beslutet

        if (move == Move.Hit)
        {
            Console.WriteLine($"{player.Name} köper.");
        }
        else
        {
            Console.WriteLine($"{player.Name} stannar.");
        }

        Thread.Sleep(1500);   //Tid att läsa beslutet
    }
    public void ShowComputerAutoChosen(string seatedName, string addedName)   //Visas när bara en sorts datorspelare finns kvar att välja
{
    Console.WriteLine($"{seatedName} sitter redan vid bordet, {addedName} läggs till.");
}
    public string AskPlayerName(string promt)
    {
        Console.Write(promt);
        string? name = Console.ReadLine()?.Trim();
        while (string.IsNullOrWhiteSpace(name) 
            || name.Contains(';')
            || name.ToUpper() == "GIVAREN"
            || name.ToUpper() == "GIVARE")
        {   
            Console.Write("Namnet får inte vara tomt, innehålla (;) eller vara Givare/Givaren: ");
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

    public Move WantsToHit(Player player) // retunerar vilket Move man väljer
    {
        ShowCount();
        while (true)
        {
            Console.Write($"{player.Name}, Köp eller Stanna?: ");
            string? input = Console.ReadLine()?.ToUpper().Trim();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input == "KÖP")
                {
                    return Move.Hit;
                }
                if (input == "STANNA")
                {
                    return Move.Stand;
                }
                Console.WriteLine("Svara med köp eller stanna");
            }
            else
            {
                Console.WriteLine("Det får inte vara tomt");
            }
        }

    }
    public void ShowOutcomeAndWinAmount(Player player, Outcome outcome, int winAmount)
    {
        string markerText = "";
        if (!player.IsComputer)
        {
            markerText = $" Får tillbaka ({winAmount} Marker)";
        }

        if (outcome == Outcome.Lose && player.Hand.IsBust)
        {
            Console.WriteLine($"{player.Name} spräckte med {player.Hand.Points} och förlorar.");
        }
        else if (outcome == Outcome.Win)
        {
            Console.WriteLine($"{player.Name} vinner!{markerText}");
        }
        else if (outcome == Outcome.Lose)
        {
            Console.WriteLine($"{player.Name} förlorar.");
        }
        else if (outcome == Outcome.Push)
        {
            Console.WriteLine($"{player.Name} och givaren har lika.{markerText}");
        }
        else if (outcome == Outcome.Blackjack)
        {
            Console.WriteLine($"{player.Name} har BLACKJACK och vinner!{markerText}");
        }
    }

    public int AskHowMuchToBet(Player player) 
    {
        Console.Clear();
        Console.WriteLine($"{player.Name}, du har ({player.Wallet.Chips} Marker)");
        Console.Write($"Hur mycket satsar du? (minst 25): ");
        int betAmount;
        while (!int.TryParse(Console.ReadLine(), out betAmount) || betAmount is < 25)
        {
            Console.Write("Skriv in ett tal som inte är under 25: ");
        }
        return betAmount;
    }

    public bool AskIfWantToBuyIn(Player player)
    {
        Console.Write($"{player.Name} du har under 25 marker. Vill du köpa dig in?(Y/N): ");
        string? answer = Console.ReadLine()?.ToUpper().Trim();
        while (answer != "Y" && answer != "N")  //Frågar igen tills svaret är Y eller N
        {
            Console.Write("Svara med Y eller N!: ");
            answer = Console.ReadLine()?.ToUpper().Trim();
        }

        if (answer == "Y")
        {
            Console.WriteLine("Du köper dig in och får 200 marker");
            Console.ReadKey();
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void PrintNotEnoughCash()
    {
        Console.WriteLine("Du har inte nog med marker");
        Console.ReadKey();
    }

    public void DealerTurnMessage()
    {
        Console.WriteLine("----Givarens tur----");
    }

    public void ShowBlackjack(Participant participant)
    {
        Console.WriteLine($"{participant.Name} fick BLACKJACK!");
        ShowCount();
    }


    public void ShowHand(Participant participant) //Visar korten, värdet om handen är mjuk eller hård.
    {
        CheckNewDeck();
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
        DrawCards(participant.Hand.Cards);  //Ritar alla kort med framsidan upp


        foreach (Card card in participant.Hand.Cards) //Låter räknaren se alla kort i handen.
        {
            _counter.ObserveCard(card); //Korträknaren ser kortet när det visas. Samma kort räknas bara en gång. Räknaren ser bara kort som spelaren ser.
        }

        if (participant.Hand.IsBust)   //IsBust är true om poängen är över 21
        {
            Console.WriteLine($"{participant.Name} spräckte!\n");
        }
    }

    public void ShowDealerHand(Dealer dealer)   //Visar givarens hand. Så länge hålkortet är dolt ritas det med baksidan upp
    {
        CheckNewDeck();
        if (dealer.IsHoleCardHidden)   //Är hålkortet fortfarande dolt?
        {
            Console.WriteLine(dealer.Name);
            DrawCards(dealer.Hand.Cards, true); //true = rita andra kortet med baksidan upp
            _counter.ObserveCard(dealer.Hand.Cards[0]); //Bara det öppna kortet räknas. Hålkortet räknas först när det vänts.
        }
        else   //Hålkortet är vänt, visa hela handen
        {
            ShowHand(dealer); //Visa dealernskort
        }
    }

    private void CheckNewDeck()
    {
        if (Deck.Current != _lastDeck)
        {
            _counter.ResetRunningCount();
            _lastDeck = Deck.Current; //Ny lek har skapats: nollställ räknaren och kom ihåg den nya leken
        }
    }

    
    public void ShowCount() //Visar korträkningen, men bara när man spelar med sko
    {
        if (Deck.UseShoe == false || Deck.Current == null) //Ingen sko eller ingen lek: visa inget
        {
            return;
        }
        Console.WriteLine($"Running count: {_counter.RunningCount}"); //Summan av alla sedda kort
        Console.WriteLine($"True count: {_counter.TrueCount(Deck.Current.CardsLeft):F1}"); //Running count delat med antal lekar kvar, en decimal
        Console.WriteLine($"Läge: {_counter.FavorableLevel(Deck.Current.CardsLeft)}\n"); //Hur gynnsamt läget är för spelaren
    }

    ////Ritar alla kort i en hand bredvid varandra (används för både spelare och givaren).
    //cards = korten som ska ritas, t.ex. spelarens Hand.Cards.
    //hideSecondCard = true ritar andra kortet med baksidan upp (givarens hålkort). Blir false om inget skickas in. 
        private void DrawCards(List<Card> cards, bool hideSecondCard = false)
    {
        for (int row = 0; row < CardRenderer.CardHeight; row++) //Går igenom kortets 7 rader uppifrån och ner. För varje rad skrivs samma rad ut för alla kort, så att korten hamnar bredvid varandra

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
                    lines = _cardRenderer.GetBackLines(); //...bygg baksidan (░░░)
                }
                else  //Annars är det ett vanligt kort som ska visas med framsidan upp
                {
                    lines = _cardRenderer.GetCardLine(cards[i]); //Bygger kortets framsida (valör och färg) och lägger raderna i lines
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
       private void WriteError(string message)   //Skriver ett felmeddelande i rött, sedan ny rad
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    }
