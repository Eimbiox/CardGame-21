class GameStorage 
{
    private readonly string _path = "Leaderboard.txt";

    public  List<Player> _players { get; private set; }= [];    //Sparar alla players från Leaderboard.txt
    public GameStorage()    //Constructor
    {
        LoadScore();
    }  

    public void SaveScore()     //Sparar allas score
    {
        List<string> lines = [];
        foreach (var player in _players)
        {
            lines.Add($"{player.Name};{player.Wins};{player.Losses};{player.Draws}");
        }
        File.WriteAllLines(_path, lines);
    }
    public void LoadScore()
    {
        foreach (string line in File.ReadLines(_path))
        {
            string[] parts = line.Split(";");
            //                      Name                Wins                Losses               Draws
            _players.Add(new Player(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3])));  //Lägger till en player i _players
            Console.WriteLine(line);
        }
        
    }

}