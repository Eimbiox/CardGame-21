class RoundBets
{
    private Dictionary<Player, int> _playerBets = [];   //Håller varje spelare bets

    public RoundBets () { } //Konstruktor

    public bool AddChips(Player player, int amount)
    {
        if (amount > player.Wallet.Chips || amount <= 0)
        {
            return false;
        }
        _playerBets.Add(player, amount);
        return true;
        
    }

    public void RemoveChips()
    {
        
    }



}