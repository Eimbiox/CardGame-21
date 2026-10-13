class Hand
{
    public List <Card> Cards = new List<Card>();

    public int Points { get; private set; }
    public bool isSoft { get; private set; }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }
    public void ClearHand()
    {
        Cards.Clear();
    }
    public void CountPoints()
    {
        //Räkna ihop korternas värde. (ess ska vara 1 or 11)
    }


}