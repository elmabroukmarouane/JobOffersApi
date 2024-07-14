namespace JobsOffer.Api.Server.Extensions.Logging;
public static class LoggingMessaging
{
    public static void LoggingMessageCritical(this ILogger self, string nameSpaceName, int statusCodeInt, string statusCode, string httpContextRequestMethod, string controllerName, string actionName, Exception exception, string pathRootApp)
    {
        var log = "[" + LogLevel.Critical + "]" + " [" + DateTimeOffset.UtcNow.ToString("dd/MM/yyyy HH:mm:ss+00:00") + "] [" + statusCodeInt + " - " + statusCode + "] [" + httpContextRequestMethod + "] [" + nameSpaceName + "." + controllerName + "Controller." + actionName + "]: Exception => " + exception + " : " + exception.Message;
        WriteLog(log, pathRootApp);
        self.LogCritical(log);
    }
    public static void LoggingMessageError(this ILogger self, string nameSpaceName, int statusCodeInt, string statusCode, string httpContextRequestMethod, string controllerName, string actionName, Exception exception, string pathRootApp)
    {
        var log = "[" + LogLevel.Error + "]" + " [" + DateTimeOffset.UtcNow.ToString("dd/MM/yyyy HH:mm:ss+00:00") + "] [" + statusCodeInt + " - " + statusCode + "] [" + httpContextRequestMethod + "] [" + nameSpaceName + "." + controllerName + "Controller." + actionName + "]: Exception => " + exception + " : " + exception.Message;
        WriteLog(log, pathRootApp);
        self.LogError(log);
    }
    public static void LoggingMessageWarning(this ILogger self, string nameSpaceName, int statusCodeInt, string statusCode, string httpContextRequestMethod, string controllerName, string actionName, object message, string pathRootApp)
    {
        var log = "[" + LogLevel.Warning + "]" + " [" + DateTimeOffset.UtcNow.ToString("dd/MM/yyyy HH:mm:ss+00:00") + "] [" + statusCodeInt + " - " + statusCode + "] [" + httpContextRequestMethod + "] [" + nameSpaceName + "." + controllerName + "Controller." + actionName + "]: " + message;
        WriteLog(log, pathRootApp);
        self.LogWarning(log);
    }
    public static void LoggingMessageInformation(this ILogger self, string nameSpaceName, int statusCodeInt, string statusCode, string httpContextRequestMethod, string controllerName, string actionName, object message, string pathRootApp)
    {
        var logLocal = "[" + LogLevel.Information + "]" + " [" + DateTimeOffset.UtcNow.ToString("dd/MM/yyyy HH:mm:ss+00:00") + "] [" + statusCodeInt + " - " + statusCode + "] [" + httpContextRequestMethod + "] [" + nameSpaceName + "." + controllerName + "Controller." + actionName + "]: " + message;
        WriteLog(logLocal, pathRootApp);
        self.LogInformation(logLocal);
    }
    public static void LoggingMessageDebug(this ILogger self, string nameSpaceName, int statusCodeInt, string statusCode, string httpContextRequestMethod, string controllerName, string actionName, object message, string pathRootApp)
    {
        var logLocal = "[" + LogLevel.Debug + "]" + " [" + DateTimeOffset.UtcNow.ToString("dd/MM/yyyy HH:mm:ss+00:00") + "] [" + statusCodeInt + " - " + statusCode + "] [" + httpContextRequestMethod + "] [" + nameSpaceName + "." + controllerName + "Controller." + actionName + "]: " + message;
        WriteLog(logLocal, pathRootApp);
        self.LogDebug(logLocal);
    }
    public static void LoggingMessageTrace(this ILogger self, string nameSpaceName, int statusCodeInt, string statusCode, string httpContextRequestMethod, string controllerName, string actionName, object message, string pathRootApp)
    {
        var logLocal = "[" + LogLevel.Trace + "]" + " [" + DateTimeOffset.UtcNow.ToString("dd/MM/yyyy HH:mm:ss+00:00") + "] [" + statusCodeInt + " - " + statusCode + "] [" + httpContextRequestMethod + "] [" + nameSpaceName + "." + controllerName + "Controller." + actionName + "]: " + message;
        WriteLog(logLocal, pathRootApp);
        self.LogTrace(logLocal);
    }

    private static void WriteLog(string log, string pathRootApp)
    {
        var directoryPath = Path.Combine(pathRootApp, "Log");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        var fullPathLog = Path.Combine(directoryPath, "Log_" + DateTimeOffset.UtcNow.ToString("ddMMyyyy") + ".log");
        using (var streamWriter = new StreamWriter(fullPathLog, true))
        {
            streamWriter.WriteLine(log);
            streamWriter.WriteLine("---------------------------------------------------------------------------------------------------------------------");
        }
    }
}
