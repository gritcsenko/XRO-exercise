using XRO.Domain;

namespace XRO.Rules;

public class HaltWhenFailedRule : BaseRule
{
    public override (bool, IEnumerable<IFact> facts) MatchCore(IReadOnlyFactsSet set) => (set.GetFacts<FailedFact>().Any(), set);

    public override void Execute(IRulesContext context, IRuleMatchResult match) => context.Halt();
}
