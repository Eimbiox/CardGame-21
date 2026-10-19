class HumanStrategy : IPlayerStrategy   //Köper så länge handen är under 17, bryr sig inte om givarens kort
{
    private readonly ConsoleUI _ui;

    public HumanStrategy(ConsoleUI ui)
    {
        _ui = ui;
    }
    public Move ChooseMove(Hand hand, Card dealerUpcard)
    {
       
        return Move.Stand;
    }
}