namespace XRO.Domain;

public class RemovedPJFact : IFact
{
    public override bool Equals(object? obj) => obj is RemovedPJFact;

    public override int GetHashCode() => GetType().GetHashCode();
}
