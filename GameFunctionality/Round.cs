class Round
{
    private readonly List<Participant> _participants;
    private readonly Deck _deck;
    private readonly ConsoleUI _ui;
    private readonly RuleSet _rules;    //Husreglerna från rules.json

    private readonly RoundBets roundBets = new RoundBets();
    public Round(List<Participant> participants, Deck deck, ConsoleUI ui, RuleSet rules)//Constructor.
    {
        _participants = participants;
        _deck = deck;
        _ui = ui;
        _rules = rules;
    }
    public void Play()
    {
        CollectBets();
        Deal();
        PlayerTurns();
        DealerTurn();
        DecideWinners();
    }

    public void CollectBets()
    {
        foreach (var participant in _participants)
        {
            if (participant is Player player && !player.IsComputer)
            {
                int playerBet = _ui.AskHowMuchToBet(player);

                while (!roundBets.PlaceBet(player, playerBet))  //Frågar igen sålänge spelaren satsar mer än vad dem har
                {
                    _ui.PrintNotEnoughCash();
                    playerBet = _ui.AskHowMuchToBet(player);
                }
            }
        }
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
        Dealer dealer = _participants.OfType<Dealer>().First();
        foreach (var participant in _participants)
        {
            if (participant is Player player)
            {
                _ui.ShowHand(player); //Visar spelaren startkort, värde och mjuk/hård innan första valet
                if (player.Hand.IsBlackjack)        // if 2: har spelaren blackjack?
                {
                    _ui.ShowCount();   //Visar korträkningen innan nästa spelare
                    continue;                       // hoppa till nästa spelare
                }

                while (player.Hand.Points < 21) //While points are under 21
                {
                    Move move = player.Decide(dealer.Hand.Cards[0]); // använder dealerns visade kort för att bestämma move

                    if (player.IsComputer)
                    {
                        _ui.ShowComputerMove(player, move);  //Visar datorns beslut med paus
                    }
                    if (move == Move.Hit) //Player hits
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

        bool dealerNeedsToPlay = false;
        foreach (Player player in _participants.OfType<Player>()) //Går igenom alla spelare i rundan en i taget (givaren räknas inte)
        {
            if (!player.Hand.IsBust && !player.Hand.IsBlackjack)    //Kollar om det finns minst en spelare som inte har blackjack eller bust
            {
                dealerNeedsToPlay = true;
                break;  //Hiitar den en spelare så avslutas loopen direkt för då måste givaren spela. 
            }
        }
        if (!dealerNeedsToPlay)
        {
            return;
        }
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

            bool dealerHas21 = dealer.Hand.Points == 21;    //Har givaren 21, oavsett hur många kort?

            //Spelaren vinner på blackjack, utom när husregeln säger att blackjack INTE slår givarens 21 av tre kort
            if (player.Hand.IsBlackjack && !dealer.Hand.IsBlackjack && (_rules.BlackjackBeats21 || !dealerHas21))
            {
                outcome = Outcome.Blackjack;
            }
            else if (player.Hand.IsBust)  //Bust always loses, even if the dealer busts
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
            else if (dealer.Hand.IsBlackjack && !player.Hand.IsBlackjack && _rules.BlackjackBeats21)    //Givarens blackjack slår spelarens 21 av tre kort, bara om husregeln säger det
            {
                outcome = Outcome.Lose;
            }
            else
            {
                outcome = Outcome.Push;
            }
            int winAmount = 0;
            if (!player.IsComputer)   //Datorer satsar inte, så de har ingen insats att betala ut
            {
                winAmount = roundBets.SettleBet(player, outcome);
            }
            _ui.ShowOutcomeAndWinAmount(player, outcome, winAmount);

            player.AddResult(outcome);

        }
    }
}