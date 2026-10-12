class Round
{
    private readonly List<Participant> _participants;
    private readonly Deck _deck;

    public Round(List<Participant> participants, Deck deck)//Constructor.
    {
        _participants = participants;
        _deck = deck;
    }
    public void Play()
    {
        Deal();
        PlayerTurns();
        DealerTurn();
        //TODO: avgörande, jämför varje spelare mot givaren
    }
    public void Deal()
    {
        //töm kort från förra rundan, ge varje spelare 2 kort och dealern
    }
    public void PlayerTurns()
    {
        //Få spelare att fråga hit elr stand om stand avlsuta turen hit lägger till kort. Avgör om dem spräckte elr ej
    }
    public void DealerTurn()
    {
        //Sålänge givarens regler säger han ska dra(under 17) dra ett kort tills dess
    }
}