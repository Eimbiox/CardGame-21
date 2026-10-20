class SimpleStrategy : IPlayerStrategy   //Köper så länge handen är under 17, bryr sig inte om givarens kort
{
    public bool IsComputer => true;

    public Move ChooseMove(Hand hand, Card dealerUpcard)
    {
        if (hand.Points < 17) //Om handen är under 17
        {
            return Move.Hit;
        }
        return Move.Stand;
    }
}