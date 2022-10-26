using XRO;
using XRO.Domain;

var context = new XRORulesContext();

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
    context.AddFacts(temperature, commandIds);


    var responses = context.ExecuteCommands();

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
