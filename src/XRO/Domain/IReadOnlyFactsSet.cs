namespace XRO.Domain;

public interface IReadOnlyFactsSet : IReadOnlyCollection<IFact>
{
    IEnumerable<T> GetFacts<T>() where T : notnull, IFact;
}
