interface IPlayerStrategy   //Gemensamt gränssnitt för alla sätt att fatta beslut, människa eller dator
{
    Move ChooseMove(Hand hand, Card dealerUpcard);
}