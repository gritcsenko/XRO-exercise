using XRO.Domain;

namespace XRO.Rules;

public class CannotLeaveHouseWithoutClothingRule : BaseFailRule
{
    private readonly IEnumerable<ClothingType> _hotClothes = new[]{
        ClothingType.Footwear,
        ClothingType.Headwear,
        ClothingType.Shirt,
        ClothingType.Pants,
    };
    private readonly IEnumerable<ClothingType> _coldClothes = new[]{
        ClothingType.Footwear,
        ClothingType.Headwear,
        ClothingType.Socks,
        ClothingType.Shirt,
        ClothingType.Jacket,
        ClothingType.Pants,
    };

    public override bool IsMatches(IReadOnlyFactsSet set)
    {
        if (set.GetFacts<InHouseFact>().Any())
        {
            return false;
        }

        var temperature = set.GetFacts<TemperatureFact>().SingleOrDefault();
        return temperature?.Type switch
        {
            TemperatureType.Hot => !MatchClothes(_hotClothes, set),
            TemperatureType.Cold => !MatchClothes(_coldClothes, set),
            _ => false,
        };
    }

    private static bool MatchClothes(IEnumerable<ClothingType> mandatory, IReadOnlyFactsSet set)
    {
        var wearing = set.GetFacts<WearClothingFact>().Select(f => f.Type).ToArray();
        return mandatory.All(t => wearing.Contains(t));
    }
}
