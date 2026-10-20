class BasicStrategy : IPlayerStrategy   //Köper så länge handen är under 17, bryr sig inte om givarens kort
{
    public Move ChooseMove(Hand hand, Card dealerUpcard)
    {
       return Move.Hit;
    }
}