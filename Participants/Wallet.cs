class Wallet
{
    public int Chips {get; private set;} 

    public const int StartingChips = 200;
    public Wallet(int chips = StartingChips)     //Konstruktor
    {
        Chips = chips;
    } 

    public bool AddChips(int amount)    
    {
        if (amount <= 0)
        {
            return false;
        }
        Chips += amount;
        return true;
    }

    public bool RemoveChips(int amount)
    {
        if (amount > Chips || amount <= 0)
        {
            return false;
        }
        Chips -= amount;
        return true;
    }
}