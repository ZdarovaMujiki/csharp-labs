using Consumer;
using MassTransit;

namespace PrincessAndContenders.Client;

public class NextContenderConsumer : IConsumer<NextContender>
{
    private readonly Princess _princess = Princess.GetInstance();

    public async Task Consume(ConsumeContext<NextContender> context)
    {
        var id = context.Message.Id;
        var name = context.Message.Name;
        Console.WriteLine(name);

        if (_princess.IsBestContender(id, name))
        {
            var points = await _princess.GetPoints(name);
            Console.WriteLine("-----");
            Console.WriteLine(points);
        }
    }
}