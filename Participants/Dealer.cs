class Dealer : Participant
{
    public bool IsHoleCardHidden { get; private set; } = true;   //Är hålkortet dolt?

    public bool HitsSoft17 { get; set; }   //Husregel från rules.json: köper givaren även på mjuk 17?

    public Dealer(): base("Givaren") { }

    public bool ShouldHit()   //Givarens regel: köp på 16 eller lägre, stanna på 17+
    {
        if (Hand.Points <= 16)  //16 eller lägre: köp alltid
        {
            return true;
        }
        if (HitsSoft17 && Hand.Points == 17 && Hand.IsSoft)   //Husregeln: köp även på mjuk 17 (ess som räknas som 11)
        {
            return true;
        }
        return false;   //Annars stanna
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