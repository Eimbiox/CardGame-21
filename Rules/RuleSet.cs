using System.Text.Json;

//Husreglerna för spelet. Läses in från rules.json när programmet startar,
//så att man kan ändra reglerna utan att kompilera om.
record RuleSet(
    bool DealerHitsSoft17,  //Drar givaren på mjuk 17? (false = givaren stannar)
    int NumberOfDecks,      //Hur många lekar skon har (minst 2). Utan sko spelas det alltid med en lek
    int ReshuffleAt,        //Vid hur många kvarvarande kort blandas det om? (minst 15)
    bool BlackjackBeats21)  //Vinner naturlig tjugoett över 21 av tre kort eller fler?
{
    //Standardreglerna, samma som i uppgiften. Används om filen saknas eller är trasig.
    public static RuleSet Default => new(
        DealerHitsSoft17: false,
        NumberOfDecks: 6,
        ReshuffleAt: 15,
        BlackjackBeats21: true);

    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,  //Det spelar ingen roll om man skriver "reshuffleAt" eller "ReshuffleAt" i filen
        RespectRequiredConstructorParameters = true   //Saknas en regel i filen räknas filen som trasig, i stället för att regeln blir 0 eller false
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

            if (rules == null || rules.NumberOfDecks < 2 || rules.ReshuffleAt < 15)
            {
                return null;    //Orimliga värden. Skon behöver minst 2 lekar, och färre än 15 kort kan ta slut mitt i en runda
            }
            return rules;   //Allt gick bra
        }
        catch (JsonException)
        {
            return null;    //Filen är inte giltig JSON, till exempel ett glömt kommatecken
        }
    }
}
