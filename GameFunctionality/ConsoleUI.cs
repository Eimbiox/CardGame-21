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
        string? name = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(name))
        {
            Console.Write("Namnet får inte vara tomt: ");
            name = Console.ReadLine();
        }
        return name;
    }
    public bool AskIfPlayAgain()
    {
        while (true)
        {
            Console.Write("Vill ni spela igen?(Ja/Nej): ");
            string? answer = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(answer))
            {
                if (answer?.ToUpper() == "JA")
                {
                    return true;
                }
                if (answer?.ToUpper() == "NEJ")
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
}