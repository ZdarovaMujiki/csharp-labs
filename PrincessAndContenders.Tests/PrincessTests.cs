using PrincessAndContenders;

namespace Tests;

public class PrincessTests
{
    [Test]
    public void GetPoints_ShouldCalculateCorrectly()
    {
        var firstContender = new Contender
        {
            Name = "A",
            Rank = 100
        };
        var thirdContender = new Contender
        {
            Name = "B",
            Rank = 98
        };
        var fifthContender = new Contender
        {
            Name = "C",
            Rank = 96
        };
        var badContender = new Contender()
        {
            Name = "D",
            Rank = 99
        };

        Assert.Multiple(() =>
        {
            Assert.That(Princess.GetPoints(firstContender), Is.EqualTo(20));
            Assert.That(Princess.GetPoints(thirdContender), Is.EqualTo(50));
            Assert.That(Princess.GetPoints(fifthContender), Is.EqualTo(100));
            Assert.That(Princess.GetPoints(badContender), Is.EqualTo(0));
            Assert.That(Princess.GetPoints(null), Is.EqualTo(10));
        });
    }
}