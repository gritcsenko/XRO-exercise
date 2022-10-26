namespace XRO.Domain;

public class WearClothingFact : IFact
{
    public WearClothingFact(ClothingType type) => Type = type;

    public ClothingType Type { get; }

    public override bool Equals(object? obj) => obj is WearClothingFact fact && Type == fact.Type;

    public override int GetHashCode() => HashCode.Combine(Type);
}
