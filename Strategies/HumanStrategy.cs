class HumanStrategy : IPlayerStrategy  // skapas för mänskliga spelare
{
    private readonly ConsoleUI _ui;
    private readonly Player _player;

    public HumanStrategy(ConsoleUI ui, Player player)
    {
        _ui = ui;
        _player = player;
    }
    public Move ChooseMove(Hand hand, Card dealerUpcard)
    {
        return _ui.WantsToHit(_player);
    }
}