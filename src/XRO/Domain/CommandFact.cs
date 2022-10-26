namespace XRO.Domain;
public class CommandFact : IFact
{
    public CommandFact(int id) => Id = id;

    public int Id { get; }
}
