using XRO.Domain;

namespace XRO.Rules;

public class MustLeaveHouseRule : BaseFailRule
{
    public override bool IsMatches(IReadOnlyFactsSet set) => set.GetFacts<InputEndsFact>().Any() && set.GetFacts<InHouseFact>().Any();
}
