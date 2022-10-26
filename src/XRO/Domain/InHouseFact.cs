namespace XRO.Domain;
public class InHouseFact : IFact
{
    public override bool Equals(object? obj) => obj is InHouseFact;

    public override int GetHashCode() => GetType().GetHashCode();
}
