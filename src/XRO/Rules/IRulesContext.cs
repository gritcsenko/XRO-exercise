using XRO.Domain;

namespace XRO.Rules;

public interface IRulesContext
{
    bool IsHalted { get; }

    void Add(IFact fact);

    void AddAll(IReadOnlyCollection<IFact> set);

    void Remove(IFact fact);

    void RemoveAll(IReadOnlyCollection<IFact> set);

    void Halt();

    IReadOnlyFactsSet Execute();
}