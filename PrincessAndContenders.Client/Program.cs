namespace PrincessAndContenders.Client;

static class Program
{
    public static async Task Main(string[] args)
    {
        var princess = new Princess();
        await princess.GetMarried();
    }
}