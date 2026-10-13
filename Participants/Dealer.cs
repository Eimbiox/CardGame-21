class Dealer : Participant
{
    public bool IsHoleCardHidden { get; private set; } = true;   //Är hålkortet dolt?

    public Dealer(): base("Givaren") { }

    public bool ShouldHit()   //Givarens regel: köp på 16 eller lägre, stanna på 17+
    {
        return Hand.Points <= 16;
    }

    public void HideHoleCard()  //Ny runda: lägg hålkortet med baksidan upp
    {
        IsHoleCardHidden = true;
    }

    public void RevealHoleCard()  //Givarens tur: vänd hålkortet
    {
        IsHoleCardHidden = false;
    }
}