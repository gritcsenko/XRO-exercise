using XRO.Domain;

namespace XRO.Rules;

public class MustLeaveHouseRule : BaseFailRule
{
    public override bool Matches(IReadOnlyFactsSet set)
    {
        if (!set.GetFacts<InputEndsFact>().Any())
        {
            return false;
        }
        if (set.GetFacts<InHouseFact>().Any())
        {
            return true;
        }
        return false;
    }
}
