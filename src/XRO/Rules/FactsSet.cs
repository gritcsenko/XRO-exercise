using System.Collections;
using XRO.Domain;

namespace XRO.Rules;

public class FactsSet : IReadOnlyFactsSet
{
    public FactsSet(IEnumerable<IFact> facts) => Facts = facts.ToArray();

    public IEnumerable<IFact> Facts { get; }

    public int Count { get; }

    public IEnumerator<IFact> GetEnumerator() => Facts.GetEnumerator();

    public IEnumerable<T> GetFacts<T>() where T : notnull, IFact => Facts.OfType<T>();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
