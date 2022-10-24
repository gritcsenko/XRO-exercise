namespace XRO;

public class PutOnBeforeRule : IRule
{
    private readonly int _putOnId;
    private readonly int _beforeId;

    public PutOnBeforeRule(int putOnId, int beforeId) =>
        (_putOnId, _beforeId) = (putOnId, beforeId);

    public bool CanApplyCommand(IRulesContext context, int commandId) =>
        commandId != _beforeId || context.AppliedCommands.Contains(_putOnId);
}