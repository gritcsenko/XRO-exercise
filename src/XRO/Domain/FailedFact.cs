namespace XRO.Domain;

public class FailedFact : IFact
{
    public override bool Equals(object? obj) => obj is FailedFact;

    public override int GetHashCode() => GetType().GetHashCode();
}
