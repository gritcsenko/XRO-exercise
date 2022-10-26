using XRO.Domain;

namespace XRO.Rules;

public class RemovePJFirstRule : BaseFailRule
{
    public override bool Matches(IReadOnlyFactsSet set)
    {
        var clothings = set.GetFacts<WearClothingFact>().Select(f => f.Type).ToArray();
        return clothings.Contains(ClothingType.Pajamas) && clothings.Length > 1;
    }
}
