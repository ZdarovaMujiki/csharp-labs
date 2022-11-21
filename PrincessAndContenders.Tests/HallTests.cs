using PrincessAndContenders;
using PrincessAndContenders.Exceptions;

namespace Tests;

public class HallTests
{
    [Test]
    public void GetNext_HallIsNotEmpty_ReturnsContender()
    {
        var contenderA = new Contender { Name = "A", Rank = 1 };
        var hall = new Hall(new Queue<Contender>(new []{ contenderA }));
        var contender = hall.GetNext();
        Assert.That(contender, Is.EqualTo(contenderA));
    }
    
    [Test]
    public void GetNext_HallIsEmpty_ThrowsEmptyHallException()
    {
        var contenderA = new Contender { Name = "A", Rank = 1 };
        var hall = new Hall(new Queue<Contender>(new []{ contenderA }));
        Assert.DoesNotThrow(() => hall.GetNext());
        Assert.Throws<EmptyHallException>(() => hall.GetNext());
    }
}