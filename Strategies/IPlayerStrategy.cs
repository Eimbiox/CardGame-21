interface IPlayerStrategy   //Gemensamt gränssnitt för alla sätt att fatta beslut, människa eller dator
{
    Move ChooseMove(Hand hand, Card dealerUpcard);

    bool IsComputer { get; }   //Varje strategi svarar själv på om det är en dator som bestämmer
}