class GameStorage 
{

    
    private readonly string _path = "Leaderboard.txt";
    public GameStorage() { }  //Constructor

    public void SaveScore(List<Player> players)
    {
        List<string> lines = [];
        foreach (var player in players)
        {
            lines.Add($"{player.Name};{player.Wins};{player.Losses};{player.Draws}");
        }
        File.WriteAllLines(_path, lines);
    }
    //playerLeaderboard[0,: 1, 2, 3,]
    public void LoadScore()
    {
        
    }
}