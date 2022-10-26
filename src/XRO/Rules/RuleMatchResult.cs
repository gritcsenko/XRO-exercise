using XRO.Domain;

namespace XRO.Rules;

internal record RuleMatchResult(IRule Rule, bool IsMatched, IReadOnlyFactsSet Set) : IRuleMatchResult
{
}
