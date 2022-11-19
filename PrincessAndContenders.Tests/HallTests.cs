using PrincessAndContenders;

namespace Tests;

public class HallTests
{
    [Test]
    public void GetNext_HallIsNotEmpty_ReturnsContender()
    {
        var hall = new Hall();
        var contender = hall.GetNext();
        Assert.That(contender, Is.InstanceOf<Contender>());
    }
    
    [Test]
    public void GetNext_HallIsEmpty_ThrowsEmptyHallException()
    {
        var hall = new Hall();
        for (var i = 0; i < Constants.ContendersAmount; ++i)
        {
            Assert.DoesNotThrow(() => hall.GetNext());
        }
        Assert.Throws<EmptyHallException>(() => hall.GetNext());
    }
}