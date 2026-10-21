using System.Text.Json;

//Husreglerna för spelet. Läses in från rules.json när programmet startar,
//så att man kan ändra reglerna utan att kompilera om.
record RuleSet(
    bool DealerHitsSoft17,  //Drar givaren på mjuk 17? (false = givaren stannar)
    int NumberOfDecks,      //Hur många lekar spelas det med?
    int ReshuffleAt,        //Vid hur många kvarvarande kort blandas det om?
    bool BlackjackBeats21)  //Vinner naturlig tjugoett över 21 av tre kort eller fler?
{
    //Standardreglerna, samma som i uppgiften. Används om filen saknas eller är trasig.
    public static RuleSet Default => new(
        DealerHitsSoft17: false,
        NumberOfDecks: 1,
        ReshuffleAt: 15,
        BlackjackBeats21: true);

    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true  //Det spelar ingen roll om man skriver "reshuffleAt" eller "ReshuffleAt" i filen
    };

    //Läser reglerna från filen. Returnerar null om filen saknas, är trasig eller har orimliga värden.
    public static RuleSet? Load(string path)
    {
        if (!File.Exists(path))     //Finns inte filen?
        {
            return null;
        }

        try     //Om något går fel här hoppar programmet till catch i stället för att krascha
        {
            string json = File.ReadAllText(path);   //Läser hela filen som text
            RuleSet? rules = JsonSerializer.Deserialize<RuleSet>(json, Options);    //Gör om texten till ett RuleSet

            if (rules == null || rules.NumberOfDecks < 1 || rules.ReshuffleAt < 1)
            {
                return null;    //Saknade eller orimliga värden, till exempel 0 lekar
            }
            return rules;   //Allt gick bra
        }
        catch (JsonException)
        {
            return null;    //Filen är inte giltig JSON, till exempel ett glömt kommatecken
        }
    }
}
