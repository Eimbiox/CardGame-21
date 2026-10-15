class Game
{
    private readonly List<Player> _players = [];    //Holds all human players
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
                //Visa leaderboard
            }
            else if (choice == 3)
            {
                 //Sök efter resultat
            }
            else
            {
                running = false;
            }
        }
    }

    public void Play()  //Starts the game
    {
        _players.Clear();
                _participants.Clear();
                int playerCount = _ui.AskNumberOfPlayers();

                for (int i = 0; i < playerCount; i++)   //Adds players based on the ammount of players.
                {
                    _players.Add(new Player(_ui.AskPlayerName(i + 1)));
                }
                foreach (Player player in _players) //Adds the players in the participants list
                {
                    _participants.Add(player);
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
                    gameStorage.SaveScore(_players);

                }
    }

    public Game(ConsoleUI ui)   //Constructor
    {
        _ui = ui;
    }
}
