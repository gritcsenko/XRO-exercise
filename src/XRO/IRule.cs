namespace XRO;

public interface IRule
{
    bool CanApplyCommand(IRulesContext context, int commandId);
}