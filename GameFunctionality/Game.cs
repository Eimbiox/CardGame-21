class Game
{
    private readonly ConsoleUI _ui;
     public void Run()
    {
        _ui.GameStart();

        int playerCount = _ui.AskNumberOfPlayers();
    }

    public Game(ConsoleUI ui)   //Constructor
    {
        _ui = ui;
    }
}
