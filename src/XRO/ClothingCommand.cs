namespace XRO;

public record ClothingCommand(string Description, IReadOnlyDictionary<TemperatureType, string> Responses);