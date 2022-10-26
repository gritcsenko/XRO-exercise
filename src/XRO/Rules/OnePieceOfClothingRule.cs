using XRO.Domain;

namespace XRO.Rules;

public class OnePieceOfClothingRule : BaseFailRule
{
    public override bool IsMatches(IReadOnlyFactsSet set) =>
        set.GetFacts<WearClothingFact>().GroupBy(f => f.Type).Any(g => g.Count() > 1);
}
