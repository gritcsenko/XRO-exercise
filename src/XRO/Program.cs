using XRO.Domain;
using XRO.Rules;

var context = new RulesContext(
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

    new HaltWhenFailedRule()
);

while (true)
{
    Console.Write("Input: ");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Input failed");
        return;
    }

    var (temperature, commandIds) = Parse(input);

    var responses = ExecuteCommands(context, temperature, commandIds);

    var output = string.Join(", ", responses);

    Console.Write("Output: ");
    Console.WriteLine(output);
}

static (TemperatureType, IEnumerable<int>) Parse(string input)
{
    var items = input.Split(' ', 2);
    var temperature = Enum.Parse<TemperatureType>(items[0], true);
    var commands = items[1].Split(',').Select(x => x.Trim()).Select(x => int.Parse(x)).ToArray();

    return (temperature, commands);
}

static IEnumerable<string> ExecuteCommands(RulesContext context, TemperatureType temperature, IEnumerable<int> commandIds)
{
    // assumptions
    context.AddAll(new IFact[] { new InHouseFact(), new WearClothingFact(ClothingType.Pajamas) });

    // user inputs
    context.Add(new TemperatureFact(temperature));
    foreach (var id in commandIds)
    {
        context.Add(new CommandFact(id));
    }
    context.Add(new InputEndsFact());

    var set = context.Execute();
    foreach (var fact in set)
    {
        if (GetResponse(fact, temperature) is string response)
        {
            yield return response;
        }
    }
}

static string? GetResponse(IFact fact, TemperatureType temperature)
{
    return fact switch
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