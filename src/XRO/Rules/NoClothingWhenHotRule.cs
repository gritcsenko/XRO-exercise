using XRO.Domain;

namespace XRO.Rules;

public class NoClothingWhenHotRule : BaseFailRule
{
    private readonly ClothingType _type;

    public NoClothingWhenHotRule(ClothingType type) => _type = type;

    public override bool IsMatches(IReadOnlyFactsSet set)
    {
        var temperature = set.GetFacts<TemperatureFact>().SingleOrDefault();
        return temperature?.Type switch
        {
            TemperatureType.Hot => set.GetFacts<WearClothingFact>().Any(f => f.Type == _type),
            _ => false,
        };
    }
}
