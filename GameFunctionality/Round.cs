using System.Runtime.InteropServices;

class Round
{
    private readonly List<Participant> _participants;
    private readonly Deck _deck;
    private readonly ConsoleUI _ui;
    public Round(List<Participant> participants, Deck deck, ConsoleUI ui)//Constructor.
    {
        _participants = participants;
        _deck = deck;
        _ui = ui;
    }
    public void Play()
    {
        Deal();
        PlayerTurns();
        DealerTurn();
        //TODO: jämför varje spelare mot givaren och kolla vem/vilka vinner
    }
    public void Deal()  //Clears the hand and gives each participant 2 cards
    {
        foreach (var participant in _participants)
        {
            participant.Hand.ClearHand();
        }
        for (int i = 0; i < 2; i++)
        {
            foreach (var participant in _participants)
            {
                participant.Hand.AddCard(_deck.TakeCard());
            }
        }
    }
    public void PlayerTurns()
    {
        //Få spelare att fråga hit elr stand om stand avlsuta turen hit lägger till kort. Avgör om dem spräckte elr ej
        foreach (var participant in _participants)
        {
            if(participant is Player player)
            {
                while (player.Hand.Points < 21)
                {
                    bool playerChoice = _ui.WantsToHit(player);
                    if (playerChoice) //Player hits
                    {
                        player.Hit(_deck);
                    }
                    else //Player stands
                    {
                        break;
                    }

                }
            }
        }
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