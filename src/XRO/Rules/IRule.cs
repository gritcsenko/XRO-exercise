using XRO.Domain;

namespace XRO.Rules;

public interface IRule
{
    IRuleMatchResult Matches(IReadOnlyFactsSet set);

    void Execute(IRulesContext context, IRuleMatchResult match);
}
