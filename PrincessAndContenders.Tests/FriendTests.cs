using PrincessAndContenders;
using PrincessAndContenders.Exceptions;

namespace Tests;

public class FriendTests
{
    [Test]
    public void Compare_ShouldCompareCorrectly()
    {
        var contenderA = new Contender { Name = "A", Rank = 1 };
        var contenderB = new Contender { Name = "B", Rank = 2 };
        var hall = new Hall(new Queue<Contender>());
        var friend = new Friend(hall);

        Assert.Multiple(() =>
        {
            Assert.That(friend.Compare(contenderA, contenderB), Is.EqualTo(-1));
            Assert.That(friend.Compare(contenderA, contenderA), Is.EqualTo(0));
            Assert.That(friend.Compare(contenderB, contenderA), Is.EqualTo(1));
        });
    }

    [Test]
    public void Compare_UnknownContender_ThrowsUnknownContenderException()
    {
        var contenderA = new Contender { Name = "A", Rank = 1 };
        var unknownContender = new Contender { Name = "B", Rank = 2 };
        var hall = new Hall(new Queue<Contender>(new []{ contenderA, unknownContender }));
        var friend = new Friend(hall);

        var knownContender = hall.GetNext();

        Assert.Throws<UnknownContenderException>(() => friend.Compare(unknownContender, knownContender));
        Assert.Throws<UnknownContenderException>(() => friend.Compare(knownContender, unknownContender));
    }
}