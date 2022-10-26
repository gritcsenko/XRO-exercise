using XRO.Domain;

namespace XRO.Rules;

public abstract class BaseFailRule : BaseRule
{
    public sealed override (bool, IEnumerable<IFact> facts) MatchCore(IReadOnlyFactsSet set) => (IsMatches(set), set.TakeLast(1));

    public sealed override void Execute(IRulesContext context, IRuleMatchResult match)
    {
        context.RemoveAll(match.Set);
        context.Add(new FailedFact());
    }

    public abstract bool IsMatches(IReadOnlyFactsSet set);
}
