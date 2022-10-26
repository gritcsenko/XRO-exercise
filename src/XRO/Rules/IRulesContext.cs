using XRO.Domain;

namespace XRO.Rules;

public interface IRulesContext
{
    bool IsHalted { get; }

    void Add(IFact fact);

    void AddAll(IEnumerable<IFact> set);

    void Remove(IFact fact);

    void RemoveAll(IEnumerable<IFact> set);

    void Halt();

    IReadOnlyFactsSet Execute();
}