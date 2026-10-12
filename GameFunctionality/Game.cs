class Game
{
    private readonly List<Player> _players = [];    //Holds all human players
    private readonly List<Participant> _participants = [];   //Holds all participants
    
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
            _participants.Add(player);
        }
        _participants.Add(_dealer);  //Adds the dealer to the participants list

        bool playing = true;
        
        while (playing)
        {
            Deck deck = new Deck();
            Round round = new Round(_participants, deck);
            round.Play();
        }
    }

    public Game(ConsoleUI ui)   //Constructor
    {
        _ui = ui;
    }
}
