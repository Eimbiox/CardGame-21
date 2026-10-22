
class Game
{
    private readonly List<Participant> _participants = [];   //Holds all participants
    private readonly GameStorage gameStorage = new GameStorage();
    private readonly Dealer _dealer = new Dealer(); //Creates new dealer
    private readonly ConsoleUI _ui;
    public void Run()
    {
        bool running = true;
        if (gameStorage.Errors.Count() > 0)
            {
                _ui.PrintErrors(gameStorage);
            }
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
            bool bothComputersSeated = _participants.OfType<Player>().Count(p => p.IsComputer) == 2;   //Båda sorterna sitter redan, platsen måste bli en människa
            bool isHuman = bothComputersSeated || _ui.AskIsHuman($"Spelare {i + 1} – ange (människa/dator): ");
            if (isHuman)
            { 
                string name = _ui.AskPlayerName($"Skriv in namnet på spelare {i + 1}: ");
                bool exists = _participants.Any(p => p.Name == name) || name.StartsWith("Dator", StringComparison.OrdinalIgnoreCase);   //Namnet är taget, eller reserverat för datorspelare

                while (exists)
                {
                    name = _ui.AskPlayerName($"Namnet {name} är upptaget eller reserverat. Skriv något unikt: ");
                    exists = _participants.Any(p => p.Name == name) || name.StartsWith("Dator", StringComparison.OrdinalIgnoreCase);
                }

                bool nameFound = false;
                foreach (var savedPlayer in gameStorage._players)   //Jämför namnet med namnen i gameStorage._players och skapar en spelare baserat på de
                {
                    if (savedPlayer.Name == name)   //Om namnet hittades
                    {
                        _participants.Add(savedPlayer);
                        savedPlayer.Strategy = new HumanStrategy(_ui, savedPlayer);
                        nameFound = true; 
                    }
                }
                if (!nameFound) //Om det inte hittades
                {
                    Player player = new Player(name);   //Skapa en ny player
                    _participants.Add(player);  //Lägg till i participants
                    player.Strategy = new HumanStrategy(_ui, player);
                    gameStorage._players.Add(player);   //Lägg till i gameStorage._players
                }
            }
            else
            {
                CreateComputerPlayer();
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
            CheckWhoToRemove();
            if (!_participants.OfType<Player>().Any())  //Kollar om det finns någon Player i _participants
            {
                _ui.ShowNoPlayersLeft();
                return;
            }
            Round round = new Round(_participants, deck, _ui);
            round.Play();
            playing = _ui.AskIfPlayAgain();
            gameStorage.SaveScore();
        }
    }

    private void CheckWhoToRemove()     //Tar bort spelare från spelet som valde att inte köpa sig in.
    {
        List<Player> playersToBeRemoved = [];

        foreach (var participant in _participants)
        {
            if (participant is Player player && player.Wallet.Chips < 25 && !player.IsComputer)
            {
                if (_ui.AskIfWantToBuyIn(player))
                {
                    player.Wallet.AddChips(200);
                }
                else
                {
                    playersToBeRemoved.Add(player);
                }
            }
        }
        foreach (var player in playersToBeRemoved)
        {
            _participants.Remove(player);
        }
    }

    private void CreateComputerPlayer()
    {
        bool simpleSeated = _participants.Any(p => p.Name == "Dator: Simpel");
        bool basicSeated = _participants.Any(p => p.Name == "Dator: Grund");

        string botChoice;
        if (simpleSeated)   //Finns bara ett val kvar, så användaren behöver inte svara
        {
            botChoice = "Grund";
            _ui.ShowComputerAutoChosen("Dator: Simpel", "Dator: Grund");
        }
        else if (basicSeated)
        {
            botChoice = "Simpel";
            _ui.ShowComputerAutoChosen("Dator: Grund", "Dator: Simpel");
        }
        else
        {
            botChoice = _ui.AskWhatBot("Vilken sorts datorspelare? (Simpel/Grund): ");
        }
        string name = "Dator: " + botChoice;

        Player? player = gameStorage._players.FirstOrDefault(p => p.Name == name);   //Finns datorspelaren sparad sedan tidigare?
        if (player == null)
        {
            player = new Player(name, chips: 0);    //Skapar en ny dator spelare med 0 chips eftersom datorer inte satsar
            gameStorage._players.Add(player);   //Läggs i sparlistan så att resultaten sparas och syns i topplistan
        }
        else    //Om datorn hittas sen innan så rensas deras marker till 0. 
        {
            player.Wallet.ClearWallet();
        }

        if (botChoice == "Simpel")
        {
            player.Strategy = new SimpleStrategy();
        }
        else
        {
            player.Strategy = new BasicStrategy();
        }
        _participants.Add(player);
    }

    public Game(ConsoleUI ui)   //Constructor
    {
        _ui = ui;
    }
}
