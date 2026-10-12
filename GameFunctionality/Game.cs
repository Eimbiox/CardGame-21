class Game
{
    private readonly List<Player> _players = [];
    private readonly List<Participant> participants = [];
    
    private readonly Dealer _dealer = new Dealer(); //Creates new dealer
    private readonly ConsoleUI _ui;
    public void Run()
    {
        _ui.GameStart();

        int playerCount = _ui.AskNumberOfPlayers();

        for (int i = 0; i < playerCount; i++)   //Adds players based on the ammount of players.
        {
            _players.Add(new Player(_ui.AskPlayerName($"Skriv namnet på spelare {i + 1}: ")));
        }
        foreach (Player player in _players) //Adds the players in the participants list
        {
            participants.Add(player);
        }
        participants.Add(_dealer);  //Adds the dealer to the participants list
    }

    public Game(ConsoleUI ui)   //Constructor
    {
        _ui = ui;
    }
}
