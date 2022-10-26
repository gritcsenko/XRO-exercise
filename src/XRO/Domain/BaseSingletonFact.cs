namespace XRO.Domain;

public abstract class BaseSingletonFact<TFact> : IFact
    where TFact : BaseSingletonFact<TFact>
{
    public override bool Equals(object? obj) => obj is TFact;

    public override int GetHashCode() => GetType().GetHashCode();
}
