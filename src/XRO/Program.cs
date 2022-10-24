using XRO;

var commands = new Dictionary<int, ClothingCommand>
{
    [1] = new("Put on footwear", Responses("sandals", "boots")),
    [2] = new("Put on headwear", Responses("sunglasses", "hat")),
    [3] = new("Put on socks", Responses("socks", "socks")),
    [4] = new("Put on shirt", Responses("shirt", "shirt")),
    [5] = new("Put on jacket", Responses("jacket", "jacket")),
    [6] = new("Put on pants", Responses("shorts", "pants")),
    [7] = new("Leave house", Responses("leaving house", "leaving house")),
    [8] = new("Take off pajamas", Responses("Removing PJs", "Removing PJs")),
};
var rules = new IRule[]{
    new OnePieceOfClothingRule(),
    new NoClothingWhenHotRule(3),
    new NoClothingWhenHotRule(5),
    new PutOnBeforeRule(3, 1),
    new PutOnBeforeRule(4, 2),
    new PutOnBeforeRule(4, 5),
    new PutOnBeforeRule(6, 1),
    new PJFirstRule(8),
};

Console.Write("Input: ");
var input = Console.ReadLine();
if (string.IsNullOrEmpty(input))
{
    return;
}

var (temperature, commandIds) = Parse(input);

var context = new RulesContext(commands, rules, temperature);
var responses = ApplyCommands(context, commandIds).ToArray();


var output = string.Join(", ", responses);

Console.Write("Output: ");
Console.WriteLine(output);

static IReadOnlyDictionary<TemperatureType, string> Responses(string hot, string cold) =>
    new Dictionary<TemperatureType, string> { [TemperatureType.Hot] = hot, [TemperatureType.Cold] = cold };

static (TemperatureType, IEnumerable<int>) Parse(string input)
{
    var items = input.Split(' ', 2);
    var temperature = Enum.Parse<TemperatureType>(items[0], true);
    var commands = items[1].Split(',').Select(x => x.Trim()).Select(x => int.Parse(x)).ToArray();

    return (temperature, commands);
}

static IEnumerable<string> ApplyCommands(RulesContext context, IEnumerable<int> commandIds)
{
    foreach (var id in commandIds)
    {
        if (!context.ApplyCommand(id))
        {
            yield return "fail";
            yield break;
        }

        yield return context.Commands[id].Responses[context.Temperature];
    }

    if (!context.AppliedCommands.Contains(7))
    {
        yield return "fail";
        yield break;
    }
}
