namespace XRO;

public class RulesContext : IRulesContext
{
    private readonly IReadOnlyCollection<IRule> _rules;
    private List<int> _appliedCommands = new();

    public RulesContext(IReadOnlyDictionary<int, ClothingCommand> commands, IReadOnlyCollection<IRule> rules, TemperatureType temperature)
    {
        Commands = commands;
        _rules = rules;
        Temperature = temperature;
    }

    public IReadOnlyDictionary<int, ClothingCommand> Commands { get; }

    public IReadOnlyCollection<int> AppliedCommands => _appliedCommands.AsReadOnly();

    public TemperatureType Temperature { get; }

    public bool ApplyCommand(int commandId)
    {
        foreach (var rule in _rules)
        {
            if (!rule.CanApplyCommand(this, commandId))
            {
                return false;
            }
        }

        _appliedCommands.Add(commandId);
        return true;
    }
}