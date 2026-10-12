class Participant
{
    public string Name { get; private set; }
    public Hand Hand { get; } = new Hand();

    public Participant(string name) //Constructor
    {
        Name = name;
    }

}