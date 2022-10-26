using XRO.Domain;

namespace XRO.Rules;

public abstract class BaseFailRule : BaseRule
{
    public sealed override (bool, IEnumerable<IFact> facts) MatchesCore(IReadOnlyFactsSet set) => (Matches(set), set.TakeLast(1));

    public new abstract bool Matches(IReadOnlyFactsSet set);

    public sealed override void Execute(IRulesContext context, IRuleMatchResult match)
    {
        context.RemoveAll(match.Set);
        context.Add(new FailedFact());
    }
}
