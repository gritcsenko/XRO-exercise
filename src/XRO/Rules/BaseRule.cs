using XRO.Domain;

namespace XRO.Rules;

public abstract class BaseRule : IRule
{
    public abstract void Execute(IRulesContext context, IRuleMatchResult match);

    public IRuleMatchResult Match(IReadOnlyFactsSet set)
    {
        var (isMatched, facts) = MatchCore(set);
        return new RuleMatchResult(this, isMatched, new FactsSet(facts));
    }

    public abstract (bool, IEnumerable<IFact> facts) MatchCore(IReadOnlyFactsSet set);
}
