namespace XRO.Domain;

public class LeftHouseFact : IFact
{
    public override bool Equals(object? obj) => obj is LeftHouseFact;

    public override int GetHashCode() => GetType().GetHashCode();
}
