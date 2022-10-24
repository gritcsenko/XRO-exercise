namespace XRO;

public class PJFirstRule : IRule
{
    private readonly int _pjId;

    public PJFirstRule(int pjId) => _pjId = pjId;

    public bool CanApplyCommand(IRulesContext context, int commandId) =>
        context.AppliedCommands.Count switch
        {
            0 => commandId == _pjId,
            _ => true
        };
}
