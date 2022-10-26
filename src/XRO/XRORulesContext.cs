using XRO.Domain;
using XRO.Rules;

namespace XRO;
public class XRORulesContext : RulesContext
{
    public XRORulesContext()
        : base(
            new MapCommandRule(),

            new OnePieceOfClothingRule(),
            new NoClothingWhenHotRule(ClothingType.Socks),
            new NoClothingWhenHotRule(ClothingType.Jacket),
            new PutOnBeforeRule(ClothingType.Socks, ClothingType.Footwear),
            new PutOnBeforeRule(ClothingType.Shirt, ClothingType.Headwear),
            new PutOnBeforeRule(ClothingType.Shirt, ClothingType.Jacket),
            new PutOnBeforeRule(ClothingType.Pants, ClothingType.Footwear),
            new RemovePJFirstRule(),
            new CannotLeaveHouseWithoutClothingRule(),
            new MustLeaveHouseRule(),

            new HaltWhenFailedRule())
    {
    }

    public void AddFacts(TemperatureType temperature, IEnumerable<int> commandIds)
    {
        // assumptions
        AddAll(new IFact[] { new InHouseFact(), new WearClothingFact(ClothingType.Pajamas) });

        Add(new TemperatureFact(temperature));
        foreach (var id in commandIds)
        {
            Add(new CommandFact(id));
        }
        Add(new InputEndsFact());
    }

    public IEnumerable<string> ExecuteCommands()
    {
        var set = Execute();
        var temperature = set.GetFacts<TemperatureFact>().Single().Type;
        foreach (var fact in set)
        {
            if (GetResponse(fact, temperature) is string response)
            {
                yield return response;
            }
        }
    }

    static string? GetResponse(IFact fact, TemperatureType temperature) =>
        fact switch
        {
            WearClothingFact clothingFact => clothingFact.Type switch
            {
                ClothingType.Footwear when temperature == TemperatureType.Hot => "sandals",
                ClothingType.Footwear when temperature == TemperatureType.Cold => "boots",
                ClothingType.Headwear when temperature == TemperatureType.Hot => "sunglasses",
                ClothingType.Headwear when temperature == TemperatureType.Cold => "hat",
                ClothingType.Socks => "socks",
                ClothingType.Shirt => "shirt",
                ClothingType.Jacket => "jacket",
                ClothingType.Pants when temperature == TemperatureType.Hot => "shorts",
                ClothingType.Pants when temperature == TemperatureType.Cold => "pants",
                _ => null,
            },
            LeftHouseFact => "leaving house",
            RemovedPJFact => "Removing PJs",
            FailedFact => "fail",
            _ => null,
        };
}
