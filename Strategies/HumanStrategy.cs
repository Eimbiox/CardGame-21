class HumanStrategy : IPlayerStrategy   //Köper så länge handen är under 17, bryr sig inte om givarens kort
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