namespace XRO;
public class OnePieceOfClothingRule : IRule
{
    public bool CanApplyCommand(IRulesContext context, int commandId) =>
        !context.AppliedCommands.Contains(commandId);
}
