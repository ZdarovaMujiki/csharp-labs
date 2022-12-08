using PrincessAndContenders;
using PrincessAndContenders.Interfaces;

namespace Tests;

public class ContendersGeneratorTests
{
    private const int ContendersAmount = 100;
    private readonly IContendersGenerator _contendersGenerator = new ContendersGenerator(ContendersAmount);
    
    [Test]
    public void GenerateContenders_ReturnsCorrectLengthArray()
    {
        var contenders = _contendersGenerator.GenerateContenders();
        Assert.That(contenders, Has.Count.EqualTo(ContendersAmount));
    }
    
    [Test]
    public void GenerateContenders_ReturnsUniqueContenders()
    {
        var contenders = _contendersGenerator.GenerateContenders();
        var contendersNamesSet = new HashSet<string>(contenders.Select(contender => contender.Name));
        Assert.That(contendersNamesSet, Has.Count.EqualTo(ContendersAmount));
    }
}