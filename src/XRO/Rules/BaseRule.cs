using XRO.Domain;

namespace XRO.Rules;

public abstract class BaseRule : IRule
{
    public abstract void Execute(IRulesContext context, IRuleMatchResult match);

    public IRuleMatchResult Matches(IReadOnlyFactsSet set)
    {
        var (isMatched, facts) = MatchesCore(set);
        return new RuleMatchResult(this, isMatched, facts);
    }

    public abstract (bool, IEnumerable<IFact> facts) MatchesCore(IReadOnlyFactsSet set);

    private class RuleMatchResult : IRuleMatchResult
    {
        public RuleMatchResult(IRule rule, bool isMatched, IEnumerable<IFact> facts)
        {
            Rule = rule;
            IsMatched = isMatched;
            Set = new FactsSet(facts);
        }

        public IRule Rule { get; }
        public bool IsMatched { get; }
        public IReadOnlyFactsSet Set { get; }
    }
}
