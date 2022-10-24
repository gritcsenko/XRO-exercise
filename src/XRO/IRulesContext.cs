namespace XRO;

public interface IRulesContext
{
    IReadOnlyCollection<int> AppliedCommands { get; }
    IReadOnlyDictionary<int, ClothingCommand> Commands { get; }
    TemperatureType Temperature { get; }
}