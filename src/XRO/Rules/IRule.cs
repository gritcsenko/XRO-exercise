using XRO.Domain;

namespace XRO.Rules;

public interface IRule
{
    IRuleMatchResult Match(IReadOnlyFactsSet set);

    void Execute(IRulesContext context, IRuleMatchResult match);
}
