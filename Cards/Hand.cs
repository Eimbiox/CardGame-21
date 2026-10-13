class Hand
{
    public List <Card> Cards = new List<Card>();

    public int Points { get; private set; }

    public bool IsSoft { get; private set; }

    public bool IsBust => Points > 21; // true om handen är över 21
   


    public void AddCard(Card card)
    {
        Cards.Add(card); //kortet lägger till i listan.
        CountPoints(); //räkna om poängen direkt
    }
    public void ClearHand()
    {
        Cards.Clear();
        CountPoints();
    }
    public void CountPoints()
    {
       int total = 0; // Summan
       int softAces = 0; //Håller koll på hur många Ace har lagt till i denna lådan.

       foreach (Card card in Cards)

        {
            total += card.Point;

            if (card.Rank == Rank.Ace)

            {
                softAces++; //Ökr med 1 för varje es som hittas.
            }
            while (total > 21 && softAces > 0)

            {
                total -= 10; //Gör om ett ess från 11 till 1
                softAces--; //Ett ess mindre som räknas 11 .
            }

            Points = total;// kopiera svaret till egenskap
            IsSoft = softAces > 0;
        }
    }


}