namespace XRO;

public class NoClothingWhenHotRule : IRule
{
    private readonly int _id;

    public NoClothingWhenHotRule(int id) => _id = id;

    public bool CanApplyCommand(IRulesContext context, int commandId) =>
        context.Temperature switch
        {
            TemperatureType.Hot => commandId != _id,
            _ => true
        };
}
