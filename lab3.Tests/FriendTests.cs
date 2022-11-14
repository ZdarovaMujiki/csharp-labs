using lab2;

namespace lab3.Tests;

public class FriendTests
{
    [Test]
    public void Compare_ShouldCompareCorrectly()
    {
        var friend = new Friend();
        var contenderA = new Contender
        {
            Name = "A",
            Rank = 1
        };
        var contenderB = new Contender
        {
            Name = "B",
            Rank = 2
        };

        Assert.Multiple(() =>
        {
            Assert.That(friend.Compare(contenderA, contenderB), Is.EqualTo(-1));
            Assert.That(friend.Compare(contenderA, contenderA), Is.EqualTo(0));
            Assert.That(friend.Compare(contenderB, contenderA), Is.EqualTo(1));
        });
    }
}