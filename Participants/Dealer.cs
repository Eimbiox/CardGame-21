class Dealer : Participant
{
    public Dealer(): base("Givaren") { }

    public bool ShouldHit()
    {
        return Hand.Points <= 16;
    }
}