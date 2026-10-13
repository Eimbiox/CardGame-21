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
        DecideWinners();
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
        foreach (var participant in _participants)
        {
            if(participant is Player player)
            {
                while (player.Hand.Points < 21) //While points are under 21
                {
                    bool playerChoice = _ui.WantsToHit(player); //Ask if player wants to hit
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

        dealer.RevealHoleCard();

        while (dealer.ShouldHit())
        {
           dealer.Hit(_deck);
        }
    }
    public void DecideWinners()
{
    Dealer dealer = _participants.OfType<Dealer>().First();

    foreach (Player player in _participants.OfType<Player>())
        {
            Outcome outcome;

            if (player.Hand.IsBust)          //Bust always loses, even if the dealer busts
                outcome = Outcome.Loose;
            else if (dealer.Hand.IsBust)
                outcome = Outcome.Win;
            else if (player.Hand.Points > dealer.Hand.Points)
                outcome = Outcome.Win;
            else if (player.Hand.Points < dealer.Hand.Points)
                outcome = Outcome.Loose;
            else
                outcome = Outcome.Push;

            _ui.ShowOutcome(player, outcome);
        }
}
}