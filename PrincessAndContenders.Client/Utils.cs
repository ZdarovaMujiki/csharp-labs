namespace PrincessAndContenders.Client;

public static class Utils
{
    public static string GetCompareUri(int attemptId, int sessionId) =>
        $"/friend/{attemptId}/compare?session={sessionId}";
    
    public static string GetResetUri(int sessionId) =>
        $"/hall/reset?session={sessionId}";
    
    public static string GetNextContenderUri(int attemptId, int sessionId) =>
        $"/hall/{attemptId}/next?session={sessionId}";
    
    public static string GetContenderRankUri(int attemptId, int sessionId) =>
        $"/hall/{attemptId}/select?session={sessionId}";
}