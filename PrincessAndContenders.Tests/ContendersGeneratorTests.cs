using PrincessAndContenders;

namespace Tests;

public class ContendersGeneratorTests
{
    private const int ContendersAmount = 100;
    
    [Test]
    public void GenerateContenders_ReturnsCorrectLengthArray()
    {
        var contenders = ContendersGenerator.GenerateContenders(ContendersAmount);
        Assert.That(contenders, Has.Length.EqualTo(ContendersAmount));
    }
    
    [Test]
    public void GenerateContenders_ReturnsUniqueContenders()
    {
        var contenders = ContendersGenerator.GenerateContenders(ContendersAmount);
        var contendersNamesSet = new HashSet<string>(contenders.Select(contender => contender.Name));
        Assert.That(contendersNamesSet, Has.Count.EqualTo(ContendersAmount));
    }
}