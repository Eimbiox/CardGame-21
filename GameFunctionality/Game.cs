class Game
{
    private readonly List<Participant> _participants = [];   //Holds all participants
    private readonly GameStorage gameStorage = new GameStorage();
    private readonly Dealer _dealer = new Dealer(); //Creates new dealer
    private readonly ConsoleUI _ui;
    public void Run()
    {
        bool running = true;
        while (running)
        {
            int choice = _ui.MainMenu();
            if (choice == 1)
            {
                Play();
            }
            else if (choice == 2)
            {
                _ui.ShowLeaderboard(gameStorage);
            }
            else if (choice == 3)
            {
                _ui.FindPlayerStats(gameStorage);
            }
            else
            {
                running = false;
            }
        }
    }

    public void Play()  //Starts the game
    {
        _participants.Clear();
        int playerCount = _ui.AskNumberOfPlayers();

        for (int i = 0; i < playerCount; i++)   //Adds players based on the ammount of players.
        {
            string name = _ui.AskPlayerName(i + 1);
            bool nameFound = false;
            foreach (var savedPlayer in gameStorage._players)   //Jämför namnet med namnen i gameStorage._players och skapar en spelare baserat på de
            {
                if (savedPlayer.Name == name)   //Om namnet hittades
                {
                    _participants.Add(savedPlayer);
                    nameFound = true;
                }
            }
            if (!nameFound) //Om det inte hittades
            {
                Player player = new Player(name);   //Skapa en ny player
                _participants.Add(player);  //Lägg till i participants
                gameStorage._players.Add(player);   //Lägg till i gameStorage._players
            }
        }
        _participants.Add(_dealer);  //Adds the dealer to the participants list
        Deck deck = new Deck();
        deck.Shuffle();
        bool playing = true;
        int reshuffleLimit = _participants.Count <= 3 ? 15 : 30;    //Fler deltagare behöver fler kort varje runda

        while (playing)
        {
            if (deck.RemainingCards < reshuffleLimit)   //Skapar ny deck vid antingen 15 kort kvar eller 30 baserat på mängden deltagare
            {
                deck = new Deck();
                deck.Shuffle();
            }
            Round round = new Round(_participants, deck, _ui);
            round.Play();
            playing = _ui.AskIfPlayAgain();
            gameStorage.SaveScore();

        }
    }

    public Game(ConsoleUI ui)   //Constructor
    {
        _ui = ui;
    }
}
