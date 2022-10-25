using XRO.Domain;

namespace XRO.Rules;
public class MapCommandRule : BaseRule
{
    public override (bool, IEnumerable<IFact> facts) MatchesCore(IReadOnlyFactsSet set)
    {
        var command = set.GetFacts<CommandFact>().LastOrDefault();
        var temperature = set.GetFacts<TemperatureFact>().SingleOrDefault();
        return (command is not null && temperature is not null, set);
    }

    public override void Execute(IRulesContext context, IRuleMatchResult match)
    {
        var command = match.Set.GetFacts<CommandFact>().Last();
        switch (command.Id)
        {
            case 1:
                context.Add(new WearClothingFact(ClothingType.Footwear));
                break;
            case 2:
                context.Add(new WearClothingFact(ClothingType.Headwear));
                break;
            case 3:
                context.Add(new WearClothingFact(ClothingType.Socks));
                break;
            case 4:
                context.Add(new WearClothingFact(ClothingType.Shirt));
                break;
            case 5:
                context.Add(new WearClothingFact(ClothingType.Jacket));
                break;
            case 6:
                context.Add(new WearClothingFact(ClothingType.Pants));
                break;
            case 7:
                context.RemoveAll(match.Set.GetFacts<InHouseFact>());
                context.Add(new LeftHouseFact());
                break;
            case 8:
                context.RemoveAll(match.Set.GetFacts<WearClothingFact>().Where(f => f.Type == ClothingType.Pajamas));
                context.Add(new RemovedPJFact());
                break;
            default:
                context.Add(new FailedFact());
                break;
        }
    }
}
