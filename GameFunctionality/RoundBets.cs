class RoundBets
{
    private Dictionary<Player, int> _playerBets = [];   //Håller varje spelare bets

    public RoundBets() { } //Konstruktor

    public bool PlaceBet(Player player, int amount) //Tar från spelares plånbok och läger instatsen på bordet
    {
        if (player.Wallet.RemoveChips(amount))
        {
            _playerBets.Add(player, amount);
            return true;
        }
        return false;
    }

    public void SettleBet(Player player, Outcome outcome)   //Skickar tillbaka chips till spelare baserat på outcome
    {
        int playerBet = _playerBets[player];

        if (outcome == Outcome.Blackjack)
        {
            int blackjackWinSum = playerBet * 3 / 2;
            player.Wallet.AddChips(blackjackWinSum + playerBet);
        }
        else if (outcome == Outcome.Win)
        {
            player.Wallet.AddChips(playerBet * 2);
        }
        else if (outcome == Outcome.Push)
        {
            player.Wallet.AddChips(playerBet);
        }
        
    }



}