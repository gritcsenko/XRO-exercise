using XRO.Domain;

namespace XRO.Rules;

public class PutOnBeforeRule : BaseFailRule
{
    private readonly ClothingType _putOn;
    private readonly ClothingType _before;

    public PutOnBeforeRule(ClothingType putOnId, ClothingType beforeId) =>
        (_putOn, _before) = (putOnId, beforeId);

    public override bool Matches(IReadOnlyFactsSet set)
    {
        var wearing = set.GetFacts<WearClothingFact>().Select(f => f.Type);

        var beforeFound = false;
        foreach (var type in wearing)
        {
            switch (type)
            {
                case var value when value == _before:
                    beforeFound = true;
                    break;
                case var value when value == _putOn:
                    return beforeFound;
            }
        }

        return false;
    }
}