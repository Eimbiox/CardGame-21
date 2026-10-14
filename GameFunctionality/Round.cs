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

        Dealer dealer = _participants.OfType<Dealer>().First();
        dealer.HideHoleCard();  //Ny runda: dölj givarens kort

        for (int i = 0; i < 2; i++)
        {
            foreach (var participant in _participants)
            {
                participant.Hand.AddCard(_deck.TakeCard());
            }
        }

        _ui.ShowDealerHand(dealer);  //Visar givarens öppna kort
    }
    public void PlayerTurns()
    {
        foreach (var participant in _participants)
        {
            if (participant is Player player)
            {

                _ui.ShowHand(player); //Visar spelaren startkort, värde och mju/hård innan första valet


                if (player.Hand.IsBlackjack)        // if 2: har spelaren blackjack?
                {
                    _ui.ShowBlackjack(player);
                    continue;                       // hoppa till nästa spelare
                }

                while (player.Hand.Points < 21) //While points are under 21
                {
                    bool playerChoice = _ui.WantsToHit(player); //Ask if player wants to hit
                    if (playerChoice) //Player hits
                    {
                        player.Hit(_deck);
                        _ui.ShowHand(player); //Visar handen igen med det nya kortet.
                    }
                    else //Player stands
                    {
                        break;
                    }

                }
            }
        }
    }
    public void DealerTurn()  //Givaren vänder hålkortet och köper tills hans regel säger stopp
    {
        _ui.DealerTurnMessage();
        Dealer dealer = _participants.OfType<Dealer>().First();

        dealer.RevealHoleCard();  //vänds efter att alla spelare är klara
        _ui.ShowHand(dealer);     //Visar givarens hand med hålkortet uppvänt

        while (dealer.ShouldHit())  //Sålänge givarens regler säger han ska dra(under 17) dra ett kort tills dess
        {
            dealer.Hit(_deck);
            _ui.ShowHand(dealer);  //Visar handen igen med det nya kortet
        }
    }
    public void DecideWinners()
    {
        Dealer dealer = _participants.OfType<Dealer>().First();

        foreach (Player player in _participants.OfType<Player>())
        {
            Outcome outcome;

            if (player.Hand.IsBust)  //Bust always loses, even if the dealer busts
            {
                outcome = Outcome.Lose;
            }
            else if (dealer.Hand.IsBust)
            {
                outcome = Outcome.Win;
            } 
            else if (player.Hand.Points > dealer.Hand.Points)
            {
                outcome = Outcome.Win;
            }
            else if (player.Hand.Points < dealer.Hand.Points)
            {
                outcome = Outcome.Lose;
            }
            else if (player.Hand.IsBlackjack && !dealer.Hand.IsBlackjack)
            {
                outcome = Outcome.Win;
            }
            else if (dealer.Hand.IsBlackjack && !player.Hand.IsBlackjack)
            {
                outcome = Outcome.Lose;
            }
            else
            {
                outcome = Outcome.Push;
            }
            _ui.ShowOutcome(player, outcome);
        }
    }
}