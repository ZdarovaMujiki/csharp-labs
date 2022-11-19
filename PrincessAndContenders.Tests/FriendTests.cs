using PrincessAndContenders;

namespace Tests;

public class FriendTests
{
    [Test]
    public void Compare_ShouldCompareCorrectly()
    {
        var hall = new Hall();
        var friend = new Friend(hall);
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

    [Test]
    public void Compare_UnknownContender_ThrowsUnknownContenderException()
    {
        var hall = new Hall();
        var friend = new Friend(hall);

        var (_, knownContender) = hall.GetNext();
        var unknownContenderA = hall.Contenders[0];
        var unknownContenderB = hall.Contenders[1];

        Assert.Throws<UnknownContenderException>(() => friend.Compare(knownContender, unknownContenderB));
        Assert.Throws<UnknownContenderException>(() => friend.Compare(unknownContenderA, knownContender));
        Assert.Throws<UnknownContenderException>(() => friend.Compare(unknownContenderA, unknownContenderB));
    }
}