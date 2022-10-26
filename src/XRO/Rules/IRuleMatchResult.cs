using XRO.Domain;

namespace XRO.Rules;

public interface IRuleMatchResult
{
    IRule Rule { get; }

    bool IsMatched { get; }

    IReadOnlyFactsSet Set { get; }
}