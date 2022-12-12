using System.Configuration;
using System.Net.Http.Json;
using PrincessAndContenders.Web.DTOs;
using static PrincessAndContenders.Client.Utils;

namespace PrincessAndContenders.Client;

public static class Api
{
    private static readonly HttpClient HttpClient = new();
    
    private static readonly string? BaseUri = ConfigurationManager.AppSettings["BaseUri"];
    private static readonly int AttemptId = Convert.ToInt32(ConfigurationManager.AppSettings["Attempt"]);
    private static readonly int SessionId = Convert.ToInt32(ConfigurationManager.AppSettings["Session"]);


    static Api() =>
        HttpClient.BaseAddress = new Uri(BaseUri ?? string.Empty);

    public static string CompareContenders(string name1, string name2)
    {
        var names = new ContendersNames { name1 = name1, name2 = name2 };
        var response = HttpClient.PostAsync(GetCompareUri(AttemptId, SessionId), JsonContent.Create(names));
        response.Wait();
        var str = response.Result.Content.ReadAsStringAsync();
        str.Wait();
        return str.Result;
    }

    public static async Task ResetSession()
    {
        await HttpClient.PostAsync(GetResetUri(SessionId), null);
    }
    
    public static async Task<string> GetNextContender()
    {
        var response = await HttpClient.PostAsync(GetNextContenderUri(AttemptId, SessionId), null);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<int> GetContenderRank()
    {
        var response = await HttpClient.PostAsync(GetContenderRankUri(AttemptId, SessionId), null);
        return Convert.ToInt32(await response.Content.ReadAsStringAsync());
    }
}