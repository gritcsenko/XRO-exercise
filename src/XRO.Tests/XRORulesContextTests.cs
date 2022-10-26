using XRO.Domain;

namespace XRO.Tests;

public class XRORulesContextTests
{
    private readonly XRORulesContext _context;

    public XRORulesContextTests()
    {
        _context = new XRORulesContext();
    }

    [Theory]
    [MemberData(nameof(SuccessfulScenarios))]
    public void Should_Pass_SuccessfulScenario(TemperatureType temperature, IEnumerable<int> commandIds, IEnumerable<IFact> expected)
    {
        // Given
        _context.AddFacts(temperature, commandIds);

        // When
        var set = _context.Execute();

        // Then
        set.Should().ContainInOrder(expected);
    }

    [Theory]
    [MemberData(nameof(FailureScenarios))]
    public void Should_Fail_FailureScenarios(TemperatureType temperature, IEnumerable<int> commandIds, IEnumerable<IFact> expected)
    {
        // Given
        _context.AddFacts(temperature, commandIds);

        // When
        var set = _context.Execute();

        // Then
        set.Should().ContainInOrder(expected);
    }

    public static TheoryData<TemperatureType, IEnumerable<int>, IEnumerable<IFact>> SuccessfulScenarios()
    {
        return new()
        {
            {
                TemperatureType.Hot,
                new[] { 8, 6, 4, 2, 1, 7 },
                new IFact[] {
                    new RemovedPJFact(),
                    new WearClothingFact(ClothingType.Pants),
                    new WearClothingFact(ClothingType.Shirt),
                    new WearClothingFact(ClothingType.Headwear),
                    new WearClothingFact(ClothingType.Footwear),
                    new LeftHouseFact()
                }
            },
            {
                TemperatureType.Cold,
                new[] { 8, 6, 3, 4, 2, 5, 1, 7 },
                new IFact[] {
                    new RemovedPJFact(),
                    new WearClothingFact(ClothingType.Pants),
                    new WearClothingFact(ClothingType.Socks),
                    new WearClothingFact(ClothingType.Shirt),
                    new WearClothingFact(ClothingType.Headwear),
                    new WearClothingFact(ClothingType.Jacket),
                    new WearClothingFact(ClothingType.Footwear),
                    new LeftHouseFact()
                }
            },
        };
    }

    public static TheoryData<TemperatureType, IEnumerable<int>, IEnumerable<IFact>> FailureScenarios()
    {
        return new()
        {
            {
                TemperatureType.Hot,
                new[] { 7 },
                new IFact[] {
                    new WearClothingFact(ClothingType.Pajamas),
                    new FailedFact(),
                }
            },
            {
                TemperatureType.Hot,
                new[] { 8, 6, 6 },
                new IFact[] {
                    new InHouseFact(),
                    new RemovedPJFact(),
                    new WearClothingFact(ClothingType.Pants),
                    new FailedFact(),
                }
            },
            {
                TemperatureType.Hot,
                new[] { 8, 6, 3 },
                new IFact[] {
                    new InHouseFact(),
                    new RemovedPJFact(),
                    new WearClothingFact(ClothingType.Pants),
                    new FailedFact(),
                }
            },
            {
                TemperatureType.Cold,
                new[] { 8, 6, 3, 4, 2, 5, 7 },
                new IFact[] {
                    new RemovedPJFact(),
                    new WearClothingFact(ClothingType.Pants),
                    new WearClothingFact(ClothingType.Socks),
                    new WearClothingFact(ClothingType.Shirt),
                    new WearClothingFact(ClothingType.Headwear),
                    new WearClothingFact(ClothingType.Jacket),
                    new FailedFact(),
                }
            },
            {
                TemperatureType.Cold,
                new[] { 8, 6, 3, 4, 2, 5, 1 },
                new IFact[] {
                    new InHouseFact(),
                    new RemovedPJFact(),
                    new WearClothingFact(ClothingType.Pants),
                    new WearClothingFact(ClothingType.Socks),
                    new WearClothingFact(ClothingType.Shirt),
                    new WearClothingFact(ClothingType.Headwear),
                    new WearClothingFact(ClothingType.Jacket),
                    new FailedFact(),
                }
            },
            {
                TemperatureType.Cold,
                new[] { 6 },
                new IFact[] {
                    new InHouseFact(),
                    new WearClothingFact(ClothingType.Pajamas),
                    new FailedFact(),
                }
            },
        };
    }
}