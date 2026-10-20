class Wallet
{
    public int Chips {get; private set;} 

    public Wallet() { } //Konstruktor

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