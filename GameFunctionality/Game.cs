class Game
{
    private readonly List<Player> _players = [];
    private readonly ConsoleUI _ui;
     public void Run()
    {
        _ui.GameStart();

        int playerCount = _ui.AskNumberOfPlayers();

        for (int i = 0; i < playerCount; i++)   //Adds players based on the ammount of players.
        {
            _players.Add(new Player(_ui.AskPlayerName($"Skriv namnet på spelare {i + 1}")));
        }
       
        new Dealer();   //Creates dealer
    }

    public Game(ConsoleUI ui)   //Constructor
    {
        _ui = ui;
    }
}
