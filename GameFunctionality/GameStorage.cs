class GameStorage 
{
    private readonly string _path = "Leaderboard.txt";

    public  List<Player> _players { get; private set; }= [];
    public GameStorage()    //Constructor
    {
        LoadScore();
    }  

    public void SaveScore()
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
            _players.Add(new Player(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3])));
            Console.WriteLine(line);
        }
        
    }

}