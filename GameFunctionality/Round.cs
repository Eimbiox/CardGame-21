using System.Runtime.InteropServices;

class Round
{
    private readonly List<Participant> _participants;
    private readonly Deck _deck;

    public Round(List<Participant> participants, Deck deck)//Constructor.
    {
        _participants = participants;
        _deck = deck;
    }
    public void Play()
    {
        Deal();
        PlayerTurns();
        DealerTurn();
        //TODO: jämför varje spelare mot givaren och kolla vem/vilka vinner
    }
    public void Deal()  //Clears the hand and (TODO: gives each player and the dealer 2 cards)
    {
        foreach (var participant in _participants)
        {
            participant.Hand.ClearHand();
        }

        
    }
    public void PlayerTurns()
    {
        //Få spelare att fråga hit elr stand om stand avlsuta turen hit lägger till kort. Avgör om dem spräckte elr ej
    }
    public void DealerTurn()
    {
        //Sålänge givarens regler säger han ska dra(under 17) dra ett kort tills dess

        Dealer dealer = _participants.OfType<Dealer>().First();

        while (dealer.ShouldHit())
        {
           dealer.Hit(_deck);
        }
    }
}