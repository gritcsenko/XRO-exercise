using XRO.Domain;

namespace XRO.Rules;

public class CannotLeaveHouseWithoutClothingRule : BaseFailRule
{
    private readonly IReadOnlyCollection<ClothingType> _hotClothes = new[]{
        ClothingType.Footwear,
        ClothingType.Headwear,
        ClothingType.Shirt,
        ClothingType.Pants,
    };
    private readonly IReadOnlyCollection<ClothingType> _coldClothes = new[]{
        ClothingType.Footwear,
        ClothingType.Headwear,
        ClothingType.Socks,
        ClothingType.Shirt,
        ClothingType.Jacket,
        ClothingType.Pants,
    };

    public override bool Matches(IReadOnlyFactsSet set)
    {
        if (set.GetFacts<InHouseFact>().Any())
        {
            return false;
        }

        var temperature = set.GetFacts<TemperatureFact>().SingleOrDefault();
        return temperature?.Type switch
        {
            TemperatureType.Hot => !MatchClothes(set.GetFacts<WearClothingFact>().Select(f => f.Type).ToArray(), _hotClothes),
            TemperatureType.Cold => !MatchClothes(set.GetFacts<WearClothingFact>().Select(f => f.Type).ToArray(), _coldClothes),
            _ => false,
        };
    }

    private static bool MatchClothes(IReadOnlyCollection<ClothingType> wearing, IReadOnlyCollection<ClothingType> mandatory) => mandatory.All(t => wearing.Contains(t));
}
