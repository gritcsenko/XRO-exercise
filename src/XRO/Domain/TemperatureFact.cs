namespace XRO.Domain;

public class TemperatureFact : IFact
{
    public TemperatureFact(TemperatureType type) => Type = type;

    public TemperatureType Type { get; }
}
