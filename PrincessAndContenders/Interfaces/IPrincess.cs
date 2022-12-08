using Microsoft.Extensions.Hosting;
using PrincessAndContenders.Data;

namespace PrincessAndContenders.Interfaces;

public interface IPrincess : IHostedService
{
    public Contender? GetMarried();
    public Contender? GetBestContender();
}