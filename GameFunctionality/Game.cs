class Game
{
    private readonly ConsoleUI _ui;
     public void Run()
    {
        _ui.GameStart();
    }

    public Game(ConsoleUI ui)
    {
        _ui = ui;
    }
}
