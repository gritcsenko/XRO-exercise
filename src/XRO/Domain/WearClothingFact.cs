namespace XRO.Domain;

public class WearClothingFact : IFact
{
    public WearClothingFact(ClothingType type) => Type = type;

    public ClothingType Type { get; }
}
