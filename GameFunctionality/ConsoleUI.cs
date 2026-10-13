class ConsoleUI
{
     public void GameStart()
    {
        Console.WriteLine("=====================================");
        Console.WriteLine("        Välkommen till Tjugoett!     ");
        Console.WriteLine("=====================================");
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
            Console.Write("Vill ni spela igen?(Ja/Nej): ");
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
        if (outcome == Outcome.Loose && player.Hand.IsBust)
        {
            Console.WriteLine($"{player.Name} spräckte med {player.Hand.Points} och förlorar.");
        }
        else if (outcome == Outcome.Win)
        {
            Console.WriteLine($"{player.Name} vinner!");
        }
        else if (outcome == Outcome.Loose)
        {
            Console.WriteLine($"{player.Name} förlorar.");
        }
        else if (outcome == Outcome.Push)
        {
            Console.WriteLine($"{player.Name} och givaren har lika.");
        }
    }
}